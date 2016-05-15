using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestApiSharp.Common.Models;

namespace RestApiSharp.Server
{
    public class ResourceObjectConverter<T> : JsonConverter
    {
        private object _obj;

        public ResourceObjectConverter(T obj)
        {
            _obj = obj;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var t = JToken.FromObject(value);
            
            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                var o = (JObject) t;
                IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();
                //o.Property("Attributes")
                o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));
                var objToken = new JObject(_obj);
                objToken.WriteTo(writer);
                o.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RestApiResourceObject);
        }
    }
}
