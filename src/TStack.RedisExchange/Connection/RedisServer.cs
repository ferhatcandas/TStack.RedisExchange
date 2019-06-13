using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Connection
{
    public class RedisServer
    {
        public RedisServer(string host, int port)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host));
            Host = host;
            Port = port;
        }
        internal string Host { get; private set; }
        internal int Port { get; private set; }
    }
}
