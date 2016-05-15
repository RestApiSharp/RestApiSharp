using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using Newtonsoft.Json;
using RestApiSharp.Common.Models;

namespace RestApiSharp.Server
{

    public class RestApiEncoder
    {
        public string EncodeDocument<T>(T obj)
        {
            var objType = obj.GetType();
            var id = GetIdPropValue(objType, obj);
            var doc = new RestApiDocument
            {
                Data = new RestApiResourceObject
                {
                    Type = "FakeType",
                    Id = id,
                    Attributes = new ExpandoObject()
                }
            };
            
            var converters = new List<JsonConverter>
            {
                new ResourceAttributesConverter<T>(obj),
                new ResourceObjectConverter<T>(obj)
            };
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                Converters = converters,
                NullValueHandling = NullValueHandling.Ignore
            });
            return json;
        }

        private static string GetIdPropValue<T>(Type objType, T obj)
        {
            var idProp = objType.GetRuntimeProperty("Id");
            string id = null;
            if (idProp != null)
            {
                if (idProp.PropertyType == typeof(string))
                {
                    id = (string)idProp.GetValue(obj);
                }
            }

            return id;
        }
    }

    public class ResourceAttributesConverter<T> : JsonConverter
    {
        private T _obj;

        public ResourceAttributesConverter(T obj)
        {
            _obj = obj;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == _obj.GetType();
        }
    }
}
