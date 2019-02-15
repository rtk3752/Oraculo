using RedisOraculo.Infra.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisOraculo.Infra.Context
{
    public class RedisContext : IRedisContext
    {
        public ConnectionMultiplexer GetConnection(string config)
        {
            return ConnectionMultiplexer.Connect(config);
        }

        public IDatabase GetDb(ConnectionMultiplexer connection)
        {
            return connection.GetDatabase();
        }
    }
}
