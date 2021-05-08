using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Connection
{
    internal class RedisContext
    {
        internal ConnectionMultiplexer ConnectionMultiplexer { get; private set; }

        public RedisContext(RedisContextConfig redisContextConfig)
        {
            ConnectionMultiplexer = new Lazy<ConnectionMultiplexer>(() =>
            {
                var configOptions = new ConfigurationOptions();
                foreach (RedisServer item in redisContextConfig.RedisServers)
                    configOptions.EndPoints.Add(item.Host, item.Port);
                configOptions.ClientName = redisContextConfig.ClientName;
                configOptions.Password = redisContextConfig.Password;
                configOptions.ConnectTimeout = redisContextConfig.ConnectTimeout;
                configOptions.SyncTimeout = redisContextConfig.SyncTimeout;
                configOptions.AbortOnConnectFail = redisContextConfig.AbortOnConnectFail;
                return ConnectionMultiplexer.Connect(configOptions);
            }).Value;

        }
    }
}
