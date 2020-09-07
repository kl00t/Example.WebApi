using Example.Client;
using Example.Service.Services;
using Example.Service.Services.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Service.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection collection)
        {
            // Services
            collection.AddScoped<ICustomerService, CustomerService>();

            // Validation
            collection.AddScoped<IAddCustomerRequestValidator, AddCustomerRequestValidator>();

            // Http handler
            collection.AddTransient<FooClientHttpHandler>();

            // TODO: Add http client.
        }
    }
}