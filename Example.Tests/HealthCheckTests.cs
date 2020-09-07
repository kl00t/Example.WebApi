using System.Net;
using System.Threading.Tasks;
using Example.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Example.Tests
{
    public sealed class HealthCheckTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public HealthCheckTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task HealthCheck_ShouldReturnsOK()
        {
            // Arrange
            const string request = "/healthcheck";
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}