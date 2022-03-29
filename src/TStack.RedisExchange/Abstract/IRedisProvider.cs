using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Abstract
{
    public interface IRedisProvider : IStringSet, ISortedSet, ISet
    {
        bool Exists(string key);
        bool Delete(string key);
        long Delete(IEnumerable<string> keys);
        long ScanAndDelete(string key,int perMatch);
        void FlushDB();
        bool Ping(int ms);
    }
}
