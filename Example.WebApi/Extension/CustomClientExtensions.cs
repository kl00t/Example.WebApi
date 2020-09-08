using System;
using Example.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.WebApi.Extension
{
    public static class CustomClientExtensions
    {
        public static void AddCustomClients(this IServiceCollection services, IConfiguration configuration)
        {
            // Http client.
            services.AddHttpClient<IUserClient, UserClient>(client =>
            {
                var apiSettings = new ApiSettings();
                configuration.GetSection(nameof(ApiSettings)).Bind(apiSettings);
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // TODO: Is this required?
                client.BaseAddress = new Uri(apiSettings.Url);
            });
        }
    }
}