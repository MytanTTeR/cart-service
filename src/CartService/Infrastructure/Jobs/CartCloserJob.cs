using System;
using System.Threading;
using System.Threading.Tasks;

using CartService.Services;

using Microsoft.Extensions.DependencyInjection;

namespace CartService.Infrastructure.Jobs
{
    public class CartCloserJob : IRecurringJob
    {
        private readonly IServiceProvider _serviceProvider;

        public CartCloserJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public string GetCronExpression() => "0 * * * *";

        public async Task RunAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope serviceScope = _serviceProvider.CreateScope())
            {
                IServiceProvider serviceProvider = serviceScope.ServiceProvider;

                var cartService = serviceProvider.GetRequiredService<ICartService>();
                await cartService.CloseEstimatedAsync();
            }
        }
    }
}
