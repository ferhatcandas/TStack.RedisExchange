using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Connection
{
    public class RedisContextConfig
    {
        public RedisContextConfig(IList<RedisServer> redisServers, string password, string clientName, bool abortConnectOnFail = false, int connectTimeOut = 15000, int syncTimeOut = 15000)
        {
            if (redisServers?.Count == 0)
                throw new ArgumentNullException(nameof(redisServers));
            RedisServers = redisServers;
            Password = password;
            ClientName = clientName;
            ConnectTimeout = connectTimeOut;
            SyncTimeout = syncTimeOut;
            AbortOnConnectFail = abortConnectOnFail;
        }
        public RedisContextConfig()
        {

        }
        internal IList<RedisServer> RedisServers { get; private set; }
        internal string Password { get; private set; }
        internal string ClientName { get; private set; }
        internal int ConnectTimeout { get; private set; }
        internal bool AbortOnConnectFail { get; private set; }
        internal int SyncTimeout { get; private set; }
        internal TimeSpan DefaultExpireTime { get; private set; }
    }

}
