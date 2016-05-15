using System;
using RestApiSharp.Common.Models;

namespace RestApiSharp.Client
{
    public class RestApiClient
    {
        public T GetDocuments<T>()
        {
            return default(T);
        }

        public RestApiDocumentCollection Get()
        {
            return default(RestApiDocumentCollection);
        }

        public T GetSingle<T>()
        {
            return default(T);
        }

        public RestApiDocument GetSingle()
        {
            return default(RestApiDocument);
        }
    }
}
