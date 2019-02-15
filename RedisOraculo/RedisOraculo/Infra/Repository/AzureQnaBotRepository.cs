using Newtonsoft.Json;
using RedisOraculo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedisOraculo.Infra.Repository
{
    public class AzureQnaBotRepository : IAzureQnaBotRepository
    {
        public async Task<string> Post(string uri, string body, string endpointKey)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "EndpointKey " + endpointKey);

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
