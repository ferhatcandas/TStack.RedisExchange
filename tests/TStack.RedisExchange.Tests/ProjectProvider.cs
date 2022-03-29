using System;
using System.Collections.Generic;
using System.Text;
using TStack.RedisExchange.Connection;
using TStack.RedisExchange.Provider;
using TStack.RedisExchange.Tests.Connection;
using TStack.RedisExchange.Tool;

namespace TStack.RedisExchange.Tests
{
    public class ProjectProvider : RedisProvider
    {
        public ProjectProvider(RedisContextConfig context, ISerializer serializer = null) : base(context, serializer)
        {
        }
    }
}
