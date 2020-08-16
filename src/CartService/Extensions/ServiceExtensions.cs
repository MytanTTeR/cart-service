using System;
using System.Collections.Generic;

using CartService.Infrastructure;
using CartService.Infrastructure.Jobs;
using CartService.Services;
using CartService.Services.Impl;

using Hangfire;
using Hangfire.MemoryStorage;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CartService.Extensions
{
    internal static class ServiceExtensions
    {
        internal static void ConfigureSwaggerCustom(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Cart service API",
                    Description = "A cart service API",
                });
            });
        }

        internal static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICartService, Services.Impl.CartService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ICartItemService, CartItemService>();
        }

        internal static void ConfigureHangfire(this IServiceCollection services)
        {
            services.AddHangfire((provider, config) => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseMemoryStorage()
            );

            services.AddHangfireServer(options =>
            {
                options.StopTimeout = TimeSpan.FromSeconds(15);
                options.ShutdownTimeout = TimeSpan.FromMinutes(15);
            });

            services.ConfigureJobs();

            services.AddHostedService<RecurringJobsHostedService>();
        }

        private static void ConfigureJobs(this IServiceCollection services)
        {
            services.AddTransient<IRecurringJob, CartCloserJob>();
            services.AddTransient<IRecurringJob, ReportJob>();

            services.AddHostedService<RecurringJobsHostedService>();
        }
    }
}
