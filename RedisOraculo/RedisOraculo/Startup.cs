using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedisOraculo.Domain.Interfaces;
using RedisOraculo.Infra.Context;
using RedisOraculo.Infra.Interfaces;
using RedisOraculo.Infra.Repository;

namespace RedisOraculo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        

        public Startup()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Adicionar DI       
            services.AddSingleton<IConfiguration>(Configuration);  
            services.AddScoped<IRedisContext, RedisContext>();
            services.AddScoped<IAzureQnaBotRepository, AzureQnaBotRepository>();

        }
    }
}
