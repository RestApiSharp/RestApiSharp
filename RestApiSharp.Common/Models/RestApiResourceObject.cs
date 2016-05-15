using System.Dynamic;
using System.Runtime.InteropServices;

namespace RestApiSharp.Common.Models
{
    public class RestApiResourceObject : RestApiResourceIdentifierObject
    {
        public ExpandoObject Attributes { get; set; }
        public RestApiRelationships Relationships { get; set; }
        public RestApiLinks Links { get; set; }
    }
}