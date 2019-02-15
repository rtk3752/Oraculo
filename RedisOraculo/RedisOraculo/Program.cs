using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RedisOraculo.Domain.Interfaces;
using RedisOraculo.Domain.ValueObject;
using RedisOraculo.Infra.Interfaces;
using StackExchange.Redis;
using System;

namespace RedisOraculo
{
    static class Program
    {

        static readonly IRedisContext redisContext;
        static readonly IAzureQnaBotRepository azureQnaBotRepository;
        static readonly string config;

        static readonly string host;
        static readonly string endpointKey;
        static readonly string kb;
        static readonly string service;

        static Program()
        {
            #region Startup/serviceProvider/Config/RedisContext

            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();         

            config = startup.Configuration["connectionString:Redis"];

            #region Configs Azure Bot
            host = startup.Configuration["azureBotService:Host"];
            endpointKey = startup.Configuration["azureBotService:EndPointKey"];
            kb = startup.Configuration["azureBotService:Kb"];
            service = startup.Configuration["azureBotService:Service"];
            #endregion



            redisContext = serviceProvider.GetService<IRedisContext>();
            azureQnaBotRepository = serviceProvider.GetService<IAzureQnaBotRepository>();
            #endregion
        }

        static void Main(string[] args)
        {            
            var redis = redisContext.GetConnection(config);

            var sub = redis.GetSubscriber();
            sub.Subscribe("perguntas", (ch, msg) =>
            {
                var message = msg.ToString();

                var id = message.ToString().Substring(0, message.IndexOf(":"));
                var question = message.Substring(msg.ToString().IndexOf(":"));

                question = "{" +
                           "   'question': '" + question + "'," +
                           "}";
                                  
                RespostaOraculo(id,question, redis);                

                Console.WriteLine(msg.ToString());
            });

            Console.ReadLine();
        }

        private async static void RespostaOraculo(string key, string question, ConnectionMultiplexer redis)
        {
            string method = "/knowledgebases/" + kb + "/generateAnswer/";
            var uri = host + service + method;
            Console.WriteLine("Calling " + uri + ".");
            var response =  await azureQnaBotRepository.Post(uri, question, endpointKey);

            var obj = JsonConvert.DeserializeObject<RootObject>(response);

            redis.GetDatabase().HashSet(key, "HoBot", obj.answers[0].answer);         
        }

    }
}
