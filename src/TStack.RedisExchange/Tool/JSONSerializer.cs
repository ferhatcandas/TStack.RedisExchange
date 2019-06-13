using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Tool
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _settings;

        public SerializerType SerializerType { get; set; }

        public JsonSerializer(JsonSerializerSettings settings)
        {
            _settings = settings;
            SerializerType = SerializerType.String;
        }
        public JsonSerializer() : this(new JsonSerializerSettings()
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        })
        {
        }
        public object SerializeObject<T>(T value)
        {
            if (value is string || value is int || value is double || value is float || value is bool)
                return value.ToString();
            return JsonConvert.SerializeObject(value, _settings);
        }
        public T DeserializeObject<T>(object json)
        {
            return JsonConvert.DeserializeObject<T>(json.ToString());
        }

        public object DeserializeObject(object value)
        {
            return JsonConvert.DeserializeObject(value.ToString());
        }

        public object DeserializeObject(object value, Type type)
        {
            return JsonConvert.DeserializeObject(value.ToString(), type);
        }

    }
}
