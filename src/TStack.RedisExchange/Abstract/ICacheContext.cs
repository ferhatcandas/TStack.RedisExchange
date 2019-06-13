using System;
using System.Collections.Generic;
using System.Text;
using TStack.RedisExchange.Tool;

namespace TStack.RedisExchange.Abstract
{
    internal interface ICacheContext
    {
        ISerializer ISerializer { get; set; }
        void Connect();
        void Disconnect();
        void Flush();
    }
}
