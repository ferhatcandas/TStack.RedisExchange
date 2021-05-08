using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Abstract
{
    public interface ISet
    {
        bool AddSet<T>(string key, T value);
        bool AddSet<T>(string key, T value, DateTime expireAt);
        bool AddSet<T>(string key, T value, TimeSpan expiresIn);
        long AddSet<T>(string key, IEnumerable<T> values);
        long AddSet<T>(string key, IEnumerable<T> values, DateTime expireAt);
        long AddSet<T>(string key, IEnumerable<T> values, TimeSpan expiresIn);
        bool RemoveSet<T>(string key, T value);
        IEnumerable<T> GetSet<T>(string key);
    }
}
