using Example.Service.Services;
using Example.Service.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Service.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection collection)
        {
            // Services
            collection.AddScoped<IOrderService, OrderService>();
            collection.AddScoped<IProductService, ProductService>();
            collection.AddScoped<ICustomerService, CustomerService>();

            // Validation
            collection.AddScoped<IAddCustomerRequestValidator, AddCustomerRequestValidator>();
            collection.AddScoped<IAddProductRequestValidator, AddProductRequestValidator>();
        }
    }
}