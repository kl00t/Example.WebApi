using AutoMapper;
using Example.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Service.IoC
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(OrderService).Assembly, typeof(OrderService).Assembly);

            // Mapping profiles.
            var configuration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomerMappingProfile());
                mc.AddProfile(new ProductMappingProfile());
                mc.AddProfile(new OrderMappingProfile());
            });

            configuration.AssertConfigurationIsValid();

            // Auto Mapper Configurations
            services.AddSingleton(configuration.CreateMapper());
        }
    }
}