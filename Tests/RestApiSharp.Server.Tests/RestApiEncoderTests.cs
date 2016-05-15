using System.Dynamic;
using Newtonsoft.Json;
using NUnit.Framework;
using RestApiSharp.Common.Models;

namespace RestApiSharp.Server.Tests
{
    [TestFixture]
    public class RestApiEncoderTests
    {
        [Test]
        public void Encode_When_Results()
        {
            //Arrange
            var doc = new RestApiDocument
            {
                Data = new RestApiResourceObject
                {
                    Type = "FakeType",
                    Id = "1",
                    Attributes = new ExpandoObject()
                }
            };

            
            //IDictionary<string, object> attr = doc.Data.Attributes
            //attr.Add("Name", "Tom");

            var json = JsonConvert.SerializeObject(doc, Formatting.Indented);
        }
    }
}
