using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestApiSharp.Client;
using RestApiSharp.Server;

namespace RestApiSharp.Samples.ConsoleClient
{
    public class Test
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var doc = new JObject();
            var txtWriter = new StringWriter();
            var serializer = new JsonSerializer();
            
            var writer = new JsonTextWriter(txtWriter)
            {
                Formatting = Formatting.Indented
            };
            writer.WriteStartObject();
            writer.WritePropertyName("cool");
            var myclass = new MyClass
            {
                Id = 4,
                Name = "stuff",
                Other = "other"
            };
            serializer.Converters.Add(new MyConverter(myclass));

            serializer.Formatting = Formatting.Indented;
            serializer.Serialize(writer, myclass);
            writer.WriteEndObject();
            var json = txtWriter.ToString();
        }
    }

    public class MyClass
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Other { get; set; }
    }

    public class MyConverter : JsonConverter
    {
        private readonly Type _type;

        public MyConverter(object obj)
        {
            _type = obj.GetType();
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var objType = value.GetType();
            if (objType != _type)
            {
                return;
            }
            writer.WriteStartObject();
            foreach (var prop in _type.GetProperties())
            {
                if (!prop.CanRead || !prop.GetGetMethod().IsPublic)
                {
                    continue;
                }
                if (string.Equals(prop.Name, "Id", StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                writer.WritePropertyName(prop.Name);
                serializer.Serialize(writer, prop.GetValue(value));
            }
            writer.WriteEndObject();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == _type;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }
    }
}
