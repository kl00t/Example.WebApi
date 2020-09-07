using System;
using System.Net.Http;

namespace Example.Client
{
    public class FooClient : IFooClient
    {
        private readonly HttpClient _httpClient;

        public FooClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
    }
}