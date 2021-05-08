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
    public abstract class BaseCacheProvider<TContext> : ICacheContext
     where TContext : RedisContextConfig
    {
        #region members
        private ConnectionMultiplexer _muxer;
        private IDatabase _database;
        private TContext _context;
        #endregion

        #region properties
        public ISerializer ISerializer { get; set; }
        public BaseCacheProvider(TContext context, ISerializer serializer = null)
        {
            _context = context;
            ISerializer = serializer == null ? new JsonSerializer() : serializer;
        }
        /// <summary>
        /// Disconnect redis cluster servers
        /// </summary>
        public void Disconnect()
        {
            return;
            if (_muxer != null && _muxer.IsConnected)
                _muxer.Close();
        }
        /// <summary>
        /// Redis connect muxer to cluester rediservers
        /// </summary>
        public void Connect()
        {
            if (_muxer == null)
                _muxer = new RedisContext(_context).ConnectionMultiplexer;
            if (_database == null)
                _database = Muxer.GetDatabase();
        }

        public void Flush() => RedisProcess(() => _database.Execute("FLUSHDB"));

        /// <summary>
        /// Represents an inter-related group of connections to redis servers
        /// </summary>
        internal ConnectionMultiplexer Muxer => _muxer;
        /// <summary>
        /// StackExchange Database
        ///  Describes functionality that is common to both standalone redis servers and redis clusters
        /// </summary>
        internal IDatabase Database => _database;
        #endregion

        internal long StringIncrement(string key, long amount = 1) => RedisProcess(() => Database.StringIncrement(key, amount));
        internal long StringDecrement(string key, long amount = 1) => RedisProcess(() => Database.StringDecrement(key, amount));
        internal void KeyExpire(string key, TimeSpan expireTime) => RedisProcess(() => Database.KeyExpire(key, expireTime));
        //SortedSet
        internal bool SortedSetAdd<T>(string key, T value, double score, ISerializer serializer) => RedisProcess(() => Database.SortedSetAdd(key, value.Serialize(serializer), score));
        internal long SortedSetAdd<T>(string key, IEnumerable<SortedSetEntity<T>> values, ISerializer serializer) => RedisProcess(() => Database.SortedSetAdd(key, values.ToSortedSetEntry<T>(serializer)));
        internal IEnumerable<T> GetSortedSet<T>(string key, int startIndex, int count, StackExchange.Redis.Order order, ISerializer serializer) => RedisProcess(() => Database.SortedSetRangeByRank(key, startIndex, count, order).Deserialize<T>(serializer));
        internal IEnumerable<SortedSetEntity<T>> GetSortedSetWithScores<T>(string key, int startIndex, int count, StackExchange.Redis.Order order, ISerializer serializer) => RedisProcess(() => Database.SortedSetRangeByRankWithScores(key, startIndex, count, order).Deserialize<T>(serializer));
        //SortedSet
        //StringSet
        internal bool StringSet<T>(string key, T value, TimeSpan? expireTime, ISerializer serializer) => RedisProcess(() => Database.StringSet(key, value.Serialize(serializer), expireTime));
        internal T GetStringSet<T>(string key, ISerializer serializer) => RedisProcess(() => Database.StringGet(key).Deserialize<T>(serializer));
        //StringSet
        //SMEMBERS
        internal bool SetAdd<T>(string key, T value, ISerializer serializer) => RedisProcess(() => Database.SetAdd(key, value.Serialize(serializer)));
        internal long SetAdd<T>(string key, IEnumerable<T> values, ISerializer serializer) => RedisProcess(() => Database.SetAdd(key, values.Serialize(serializer)));
        internal IEnumerable<T> GetSet<T>(string key, ISerializer serializer) => RedisProcess(() => Database.SetMembers(key).Deserialize<T>(serializer));
        internal bool SetRemove<T>(string key, T value, ISerializer serializer) => RedisProcess(() => Database.SetRemove(key, value.Serialize(serializer)));
        //SMEMBERS
        internal bool KeyDelete(string key) => RedisProcess(() => Database.KeyDelete(key));
        internal bool KeyExists(string key) => RedisProcess(() => Database.KeyExists(key));
        internal long KeyDelete(RedisKey[] keys) => RedisProcess(() => Database.KeyDelete(keys));
        internal RedisResult Execute(string command, params object[] args) => RedisProcess(() => Database.Execute(command, args));

        #region helper
        private T RedisProcess<T>(Func<T> func)
        {
            Connect();
            var response = func();
            Disconnect();
            return response;
        }
        #endregion
    }
}
