using RedisOraculo.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedisOraculo.Domain.Interfaces
{
    public interface IAzureQnaBotRepository
    {
        Task<string> Post(string uri, string body, string endpointKey);        
    }
}
