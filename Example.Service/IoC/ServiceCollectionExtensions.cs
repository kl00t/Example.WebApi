using Example.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Service.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection collection)
        {
            collection.AddScoped<IOrderService, OrderService>();
            collection.AddScoped<IProductService, ProductService>();
            collection.AddScoped<ICustomerService, CustomerService>();
            //collection.AddScoped<IAddPatientRequestValidator, AddPatientRequestValidator>();
        }
    }
}