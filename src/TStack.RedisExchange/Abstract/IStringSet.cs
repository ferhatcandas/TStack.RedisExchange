using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Abstract
{
    public interface IStringSet
    {
        T GetString<T>(string key);

        bool Add<T>(string key, T value);
        bool Add<T>(string key, T value, TimeSpan expiresIn);
        bool Add<T>(string key, T value, DateTime expireAt);
        long Increment(string key, long amount = 1);
        long Increment(string key, DateTime expiresAt, long amount = 1);
        long Increment(string key, TimeSpan expiresIn, long amount = 1);
        long Decrement(string key, long amount = 1);
    }
}
