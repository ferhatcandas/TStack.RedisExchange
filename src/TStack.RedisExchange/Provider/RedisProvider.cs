using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Redis;
using TStack.RedisExchange.Abstract;
using TStack.RedisExchange.Connection;
using TStack.RedisExchange.Model;
using TStack.RedisExchange.Tool;

namespace TStack.RedisExchange.Provider
{
    public class RedisProvider : BaseCacheProvider<RedisContextConfig>, IRedisProvider
    {
        public RedisProvider(RedisContextConfig context, ISerializer serializer = null) : base(context, serializer)
        {

        }
        /// <summary>
        /// Adds StringSet 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add<T>(string key, T value) => StringSet<T>(key, value, null, ISerializer);
        /// <summary>
        /// Adds StringSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public bool Add<T>(string key, T value, TimeSpan expiresIn) => StringSet<T>(key, value, expiresIn, ISerializer);
        /// <summary>
        /// Adds StringSet 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireAt"></param>
        /// <returns></returns>
        public bool Add<T>(string key, T value, DateTime expireAt) => StringSet<T>(key, value, expireAt.ToTimeSpan(), ISerializer);
        /// <summary>
        /// Adds SortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool AddSortedSet<T>(string key, T value, long score) => SortedSetAdd<T>(key, value, score, ISerializer);

        /// <summary>
        /// Adds SortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <param name="expireAt"></param>
        /// <returns></returns>
        public bool AddSortedSet<T>(string key, T value, long score, DateTime expireAt)
        {
            bool returnValue = SortedSetAdd<T>(key, value, score, ISerializer);
            if (returnValue)
                KeyExpire(key, expireAt.ToTimeSpan());
            return returnValue;
        }
        /// <summary>
        /// Adds SortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public bool AddSortedSet<T>(string key, T value, long score, TimeSpan expiresIn)
        {
            bool returnValue = SortedSetAdd<T>(key, value, score, ISerializer);
            if (returnValue)
                KeyExpire(key, expiresIn);
            return returnValue;
        }
        /// <summary>
        /// Adds SortedSet list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public long AddSortedSet<T>(string key, IEnumerable<SortedSetEntity<T>> values) => SortedSetAdd<T>(key, values, ISerializer);
        /// <summary>
        /// Adds SortedSet list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="expireAt"></param>
        /// <returns></returns>
        public long AddSortedSet<T>(string key, IEnumerable<SortedSetEntity<T>> values, DateTime expireAt)
        {
            var returnValue = SortedSetAdd<T>(key, values, ISerializer);
            KeyExpire(key, expireAt.ToTimeSpan());
            return returnValue;
        }
        /// <summary>
        /// Adds SortedSet list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public long AddSortedSet<T>(string key, IEnumerable<SortedSetEntity<T>> values, TimeSpan expiresIn)
        {
            var returnValue = SortedSetAdd<T>(key, values, ISerializer);
            KeyExpire(key, expiresIn);
            return returnValue;
        }
        public bool SortedSetRemove<T>(string key, T value) => SortedSetRemove<T>(key, value, ISerializer);
        /// <summary>
        /// Add SMembers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long AddSet<T>(string key, IEnumerable<T> values) => SetAdd<T>(key, values, ISerializer);
        /// <summary>
        /// Add SMembers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddSet<T>(string key, T value) => SetAdd<T>(key, value, ISerializer);
        /// <summary>
        /// Add SMembers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireAt"></param>
        /// <returns></returns>
        public bool AddSet<T>(string key, T value, DateTime expireAt)
        {
            bool returnValue = SetAdd<T>(key, value, ISerializer);
            if (returnValue)
                KeyExpire(key, expireAt.ToTimeSpan());
            return returnValue;
        }
        /// <summary>
        /// Add SMembers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public bool AddSet<T>(string key, T value, TimeSpan expiresIn)
        {
            bool returnValue = SetAdd<T>(key, value, ISerializer);
            if (returnValue)
                KeyExpire(key, expiresIn);
            return returnValue;
        }
        /// <summary>
        /// Add SMembers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="expireAt"></param>
        /// <returns></returns>
        public long AddSet<T>(string key, IEnumerable<T> values, DateTime expireAt)
        {
            long returnValue = SetAdd<T>(key, values, ISerializer);
            KeyExpire(key, expireAt.ToTimeSpan());
            return returnValue;
        }
        /// <summary>
        /// Add SMember
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public long AddSet<T>(string key, IEnumerable<T> values, TimeSpan expiresIn)
        {
            long returnValue = SetAdd<T>(key, values, ISerializer);
            KeyExpire(key, expiresIn);
            return returnValue;
        }
        /// <summary>
        /// remove set value from key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool RemoveSet<T>(string key, T value) => SetRemove(key, value, ISerializer);





        /// <summary>
        /// Decrement String Key's value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public long Decrement(string key, long amount = 1) => StringDecrement(key, amount);
        /// <summary>
        /// Get SMembers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<T> GetSet<T>(string key) => GetSet<T>(key, ISerializer);
        /// <summary>
        /// Get String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetString<T>(string key) => GetStringSet<T>(key, ISerializer);
        /// <summary>
        /// Get SortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public IEnumerable<T> GetSortedSets<T>(string key) => GetSortedSet<T>(key, 0, -1, StackExchange.Redis.Order.Ascending, ISerializer);
        /// <summary>
        /// Get SortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<T> GetSortedSets<T>(string key, int startIndex, int count, Tool.Order order = Tool.Order.Ascending) => GetSortedSet<T>(key, startIndex, count, order.ToRedisOrder(), ISerializer);
        /// <summary>
        /// Get SortedSet Collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<SortedSetEntity<T>> GetSortedSetsAll<T>(string key, Tool.Order order = Tool.Order.Ascending) => GetSortedSetWithScores<T>(key, 0, -1, StackExchange.Redis.Order.Ascending, ISerializer);
        /// <summary>
        /// Get SortedSet Collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<SortedSetEntity<T>> GetSortedSetsAll<T>(string key, int startIndex, int count, Tool.Order order = Tool.Order.Ascending) => GetSortedSetWithScores<T>(key, startIndex, count, order.ToRedisOrder(), ISerializer);
        /// <summary>
        /// Increment String Key's value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public long Increment(string key, long amount = 1) => StringIncrement(key, amount);
        /// <summary>
        /// Increment String Key's value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiresAt"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public long Increment(string key, DateTime expiresAt, long amount = 1)
        {
            long returnValue = StringIncrement(key, amount);
            KeyExpire(key, expiresAt.ToTimeSpan());
            return returnValue;

        }
        /// <summary>
        /// Increment String Key's value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiresIn"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public long Increment(string key, TimeSpan expiresIn, long amount = 1)
        {
            long returnValue = StringIncrement(key, amount);
            KeyExpire(key, expiresIn);
            return returnValue;
        }
        /// <summary>
        /// if key exists return true
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key) => KeyExists(key);
        /// <summary>
        /// delete key with values
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string key) => KeyDelete(key);
        /// <summary>
        /// delete keys with values
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long Delete(IEnumerable<string> keys) => KeyDelete(keys.ToRedisKeys());
        /// <summary>
        /// Scan key and matches delete from redis
        /// </summary>
        /// <param name="matchKey"></param>
        /// <param name="perMatch"></param>
        /// <returns></returns>
        public long ScanAndDelete(string matchKey, int perMatch)
        {
            int nextCursor = 0;
            long totalDeleted = 0;
            do
            {
                RedisResult redisResult = Execute("SCAN", nextCursor.ToString(), "MATCH", matchKey, "COUNT", perMatch.ToString());
                var response = (RedisResult[])redisResult;
                nextCursor = int.Parse((string)response[0]);
                List<string> resultLines = ((string[])response[1]).ToList();
                KeyDelete(resultLines.Select(x => (RedisKey)x).ToArray());
                totalDeleted += resultLines.Count;
            }
            while (nextCursor != 0);
            return totalDeleted;
        }

        public void FlushDB() => Flush();


    }
}
