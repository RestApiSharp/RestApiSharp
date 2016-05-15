using System.IO;
using Newtonsoft.Json;

namespace RestApiSharp.Server
{

    public class RestApiEncoder
    {
        public string EncodeDocument<T>(T obj)
        {
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented
            };

            using (var txtWriter = new StringWriter())
            using (var writer = new JsonTextWriter(txtWriter))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WriteEndObject();
                var json = txtWriter.ToString();
                return json;
            }
        }

        //private static string GetIdPropValue<T>(Type objType, T obj)
        //{
        //    var idProp = objType.GetRuntimeProperty("Id");
        //    string id = null;
        //    if (idProp != null)
        //    {
        //        if (idProp.PropertyType == typeof(string))
        //        {
        //            id = (string)idProp.GetValue(obj);
        //        }
        //    }

        //    return id;
        //}
    }
}
