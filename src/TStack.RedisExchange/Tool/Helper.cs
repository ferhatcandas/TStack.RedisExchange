using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TStack.RedisExchange.Model;

namespace TStack.RedisExchange.Tool
{
    public enum Order
    {
        //
        // Summary:
        //     Ordered from low values to high values
        Ascending = 0,
        //
        // Summary:
        //     Ordered from high values to low values
        Descending = 1
    }
    internal static class Extensions
    {
        public static TimeSpan ToTimeSpan(this DateTime dateTime)
        {
            long diff = 0;
            if (dateTime > DateTime.Now)
                diff = (dateTime - DateTime.Now).Ticks;
            return new TimeSpan(diff);
        }
        public static RedisKey[] ToRedisKeys(this IEnumerable<string> keys)
        {
            RedisKey[] redisKeys = new RedisKey[keys.Count()];
            for (int i = 0; i < keys.Count(); i++)
                redisKeys[i] = keys.ElementAt(i);
            return redisKeys;
        }
        public static RedisValue Serialize<T>(this T value, ISerializer serializer)
        {
            RedisValue redisValue;
            switch (serializer.SerializerType)
            {
                case SerializerType.String:
                    redisValue = (string)serializer.SerializeObject<T>(value);
                    break;
                case SerializerType.ByteArray:
                    redisValue = (byte[])serializer.SerializeObject<T>(value);
                    break;
                default:
                    redisValue = (string)serializer.SerializeObject<T>(value);
                    break;
            }
            return redisValue;
        }
        public static T Deserialize<T>(this RedisValue value, ISerializer serializer) => serializer.DeserializeObject<T>(value);
        public static IEnumerable<T> Deserialize<T>(this RedisValue[] values, ISerializer serializer)
        {
            List<T> list = new List<T>();
            foreach (var value in values)
                list.Add(value.Deserialize<T>(serializer));
            return list;
        }
        public static IEnumerable<SortedSetEntity<T>> Deserialize<T>(this SortedSetEntry[] values, ISerializer serializer)
        {
            List<SortedSetEntity<T>> list = new List<SortedSetEntity<T>>();
            foreach (var value in values)
                list.Add(new SortedSetEntity<T>(value.Element.Deserialize<T>(serializer), value.Score));
            return list;
        }
        public static RedisValue[] Serialize<T>(this IEnumerable<T> values, ISerializer serializer)
        {
            RedisValue[] redisValues = new RedisValue[values.Count()];
            for (int i = 0; i < values.Count(); i++)
                redisValues[i] = values.ElementAt(i).Serialize(serializer);
            return redisValues;
        }
        public static SortedSetEntry[] ToSortedSetEntry<T>(this IEnumerable<SortedSetEntity<T>> sortedSets, ISerializer serializer)
        {
            SortedSetEntry[] sorteds = new SortedSetEntry[sortedSets.Count()];
            for (int i = 0; i < sortedSets.Count(); i++)
            {
                RedisValue redisValue = sortedSets.ElementAt(i).Item.Serialize(serializer);
                SortedSetEntry setEntry = new SortedSetEntry(redisValue, sortedSets.ElementAt(i).Score);
                sorteds[i] = setEntry;
            }
            return sorteds;
        }
        public static StackExchange.Redis.Order ToRedisOrder(this TStack.RedisExchange.Tool.Order order)
        {
            return (StackExchange.Redis.Order)order;
        }
    }
}
