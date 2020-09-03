using AutoMapper;
using Example.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Service.IoC
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CustomerService).Assembly, typeof(CustomerService).Assembly);

            // Mapping profiles.
            var configuration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomerMappingProfile());
            });

            configuration.AssertConfigurationIsValid();

            // Auto Mapper Configurations
            services.AddSingleton(configuration.CreateMapper());
        }
    }
}