using System;
using System.Threading;
using System.Threading.Tasks;

using CartService.Services;

using Microsoft.Extensions.DependencyInjection;

namespace CartService.Infrastructure.Jobs
{
    public class ReportJob : IRecurringJob
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public string GetCronExpression() => "0 0 * * *";

        public async Task RunAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope serviceScope = _serviceProvider.CreateScope())
            {
                IServiceProvider serviceProvider = serviceScope.ServiceProvider;

                var reportService = serviceProvider.GetRequiredService<IReportService>();
                await reportService.SaveReportAsync();
            }
        }
    }
}
