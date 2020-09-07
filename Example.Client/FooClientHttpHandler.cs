using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Example.Client
{
    public class FooClientHttpHandler : DelegatingHandler
    {
        private readonly ILogger<FooClientHttpHandler> _logger;

        public FooClientHttpHandler(ILogger<FooClientHttpHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}