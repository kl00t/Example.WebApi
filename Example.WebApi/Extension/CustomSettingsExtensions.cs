using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.WebApi.Extension
{
    public static class CustomSettingsExtensions
    {
        public static IServiceCollection AddCustomSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<ApiSettings>(options => configuration.GetSection(nameof(ApiSettings)).Bind(options));
            return services;
        }
    }
}