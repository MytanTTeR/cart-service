using System.Threading;
using System.Threading.Tasks;

namespace CartService.Infrastructure.Jobs
{
    interface IRecurringJob
    {
        string GetCronExpression();

        Task RunAsync(CancellationToken stoppingToken);
    }
}
