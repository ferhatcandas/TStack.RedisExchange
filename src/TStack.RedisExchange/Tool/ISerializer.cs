using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Tool
{
    public interface ISerializer
    {
        SerializerType SerializerType { get; set; }
        object SerializeObject<T>(T value);
        T DeserializeObject<T>(object value);
        object DeserializeObject(object value, Type type);
        object DeserializeObject(object value);
    }
    public enum SerializerType
    {
        String,
        ByteArray
    }
}
