using CartService.Data.Configs;
using CartService.Data.Infrastructure;
using CartService.Data.Repository;
using CartService.Data.Repository.Impl;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartService.Data.Extensions
{
    internal static class ServiceExtensions
    {
        public static void ConfigureData(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureConnectionProvider(configuration);
            services.ConfigureRepositories();
        }

        public static void ConfigureConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new CartDbConfig
            {
                ConnectionString = configuration.GetConnectionString("Cart")
            };
            services.AddSingleton(config);

            services.AddScoped<IConnectionProvider, ConnectionProvider>();
        }

        private static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
