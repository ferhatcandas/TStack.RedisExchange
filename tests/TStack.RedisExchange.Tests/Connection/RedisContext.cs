using System;
using System.Collections.Generic;
using System.Text;
using TStack.RedisExchange.Connection;

namespace TStack.RedisExchange.Tests.Connection
{
    public class RedisContext : RedisContextConfig
    {
        public RedisContext() : base(new List<RedisServer> { new RedisServer("localhost", 6379)}, "", "ClientName", 15000, 15000)
        {
        }
    }
}
