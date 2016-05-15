using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace RestApiSharp.Server.Tests
{
    [TestFixture]
    public class RestApiEncoderTests
    {
        [Test]
        public void Encode_WhenCalled_RootIsJsonObject()
        {
            //Arrange
            var encoder = new RestApiEncoder();

            //Act
            var json = encoder.EncodeDocument(new FakeObject());

            Assert.That(JObject.Parse(json), Is.Not.Null);
        }
    }

    public class FakeObject
    {
        
    }
}
