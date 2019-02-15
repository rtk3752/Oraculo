using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisOraculo.Infra.Interfaces
{
    public interface IRedisContext
    {
        ConnectionMultiplexer GetConnection(string config);
        IDatabase GetDb(ConnectionMultiplexer connection);
    }
}
