using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Example.Client.Models;
using Newtonsoft.Json;

namespace Example.Client
{
    public class UserClient : IUserClient
    {
        private readonly HttpClient _httpClient;

        public UserClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private async Task<string> GetAuthToken()
        {
            using var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("token", UriKind.Relative)
            };

            using var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception(httpResponseMessage.ReasonPhrase);
            }

            var tokenResponse = await httpResponseMessage.Content.ReadAsAsync<TokenResponse>();
            return tokenResponse.Token;
        }

        public async Task<string> CreateUser(User user)
        {
            var authToken = await GetAuthToken();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                //Content = new StringContent(), // TODO: must be posted as form data.
                RequestUri = new Uri("users", UriKind.Relative)
            };

            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            using var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsAsync<string>(); // TODO: What is the response type?
            }

            throw new Exception(httpResponseMessage.ReasonPhrase);
        }

        public async Task<string> GetUser(long userId)
        {
            var authToken = await GetAuthToken();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"users/{userId}", UriKind.Relative),
            };

            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            using var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsAsync<string>();
            }

            throw new Exception(httpResponseMessage.ReasonPhrase);
        }

        public async Task<string> DeleteUser(long userId)
        {
            var authToken = await GetAuthToken();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"users/{userId}", UriKind.Relative)
            };

            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            using var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsAsync<string>();
            }

            throw new Exception(httpResponseMessage.ReasonPhrase);
        }

        public async Task<string> UpdateUser(long userId, User user)
        {
            var authToken = await GetAuthToken();

            var jsonRequest = JsonConvert.SerializeObject(user);

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"),
                RequestUri = new Uri($"users/{userId}", UriKind.Relative)
            };

            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            using var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsAsync<string>();
            }

            throw new Exception(httpResponseMessage.ReasonPhrase);
        }
    }
}