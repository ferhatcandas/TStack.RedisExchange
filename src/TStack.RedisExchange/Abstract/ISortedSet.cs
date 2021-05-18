using System;
using System.Collections.Generic;
using System.Text;
using TStack.RedisExchange.Model;
using TStack.RedisExchange.Tool;

namespace TStack.RedisExchange.Abstract
{
    public interface ISortedSet
    {
        IEnumerable<T> GetSortedSets<T>(string key);
        IEnumerable<T> GetSortedSets<T>(string key, long startIndex, long count, Order order = Order.Ascending);
        IEnumerable<SortedSetEntity<T>> GetSortedSetsAll<T>(string key, long startIndex, long count, Order order = Order.Ascending);
        IEnumerable<SortedSetEntity<T>> GetSortedSetsAll<T>(string key, Order order = Order.Ascending);

        bool AddSortedSet<T>(string key, T value, long score);
        bool ReplaceSortedSet<T>(string key, T value, long score);
        bool AddSortedSet<T>(string key, T value, long score, DateTime expireAt);
        bool ReplaceSortedSet<T>(string key, T value, long score, DateTime expireAt);
        bool AddSortedSet<T>(string key, T value, long score, TimeSpan expiresIn);
        bool ReplaceSortedSet<T>(string key, T value, long score, TimeSpan expiresIn);
        long AddSortedSet<T>(string key, IEnumerable<SortedSetEntity<T>> values);
        long AddSortedSet<T>(string key, IEnumerable<SortedSetEntity<T>> values, DateTime expireAt);
        long AddSortedSet<T>(string key, IEnumerable<SortedSetEntity<T>> values, TimeSpan expiresIn);
        bool SortedSetRemove<T>(string key, T value);
        void SortedSetRemoveByScore(string key, long startScore, long endScore);
    }
}
