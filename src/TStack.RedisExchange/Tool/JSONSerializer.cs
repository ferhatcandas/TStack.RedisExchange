using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Tool
{
    public class JsonSerializer : ISerializer
    {
        private readonly System.Text.Json.JsonSerializerOptions _settings;

        public SerializerType SerializerType { get; set; }

        public JsonSerializer(System.Text.Json.JsonSerializerOptions settings)
        {
            _settings = settings;
            SerializerType = SerializerType.String;
        }
        public JsonSerializer() : this(new System.Text.Json.JsonSerializerOptions()
        {

        })
        {
        }
        public object SerializeObject<T>(T value)
        {
            if (value is string || value is int || value is double || value is float || value is bool || value is decimal)
                return value.ToString();
            return System.Text.Json.JsonSerializer.Serialize(value, _settings);
        }
        public T DeserializeObject<T>(object value)
        {
            var typeofT = typeof(T);
            if (typeofT == typeof(string) || typeofT == typeof(int) || typeofT == typeof(double) || typeofT == typeof(float) || typeofT == typeof(bool) || typeofT == typeof(decimal))
                return (T)Convert.ChangeType(value,typeofT);
            if (((StackExchange.Redis.RedisValue)value).IsNullOrEmpty)
                return default(T);
            return System.Text.Json.JsonSerializer.Deserialize<T>(value.ToString());
        }

        public object DeserializeObject(object value)
        {
            return System.Text.Json.JsonSerializer.Deserialize<object>(value.ToString());
        }

        public object DeserializeObject(object value, Type type)
        {
            return System.Text.Json.JsonSerializer.Deserialize(value.ToString(), type);
        }

    }
}
