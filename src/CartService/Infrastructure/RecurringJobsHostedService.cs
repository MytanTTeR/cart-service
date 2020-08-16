using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CartService.Infrastructure.Jobs;

using Hangfire;

using Microsoft.Extensions.Hosting;

namespace CartService.Infrastructure
{
    internal class RecurringJobsHostedService : BackgroundService
    {
        private readonly IRecurringJobManager _recurringJobsManager;
        private readonly IEnumerable<IRecurringJob> _recurringJobs;

        public RecurringJobsHostedService(IRecurringJobManager recurringJobsManager, IEnumerable<IRecurringJob> recurringJobs)
        {
            _recurringJobsManager = recurringJobsManager;
            _recurringJobs = recurringJobs;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RegisterRecurringJobs(stoppingToken);

            return Task.CompletedTask;
        }

        private void RegisterRecurringJobs(CancellationToken stoppingToken)
        {
            foreach (IRecurringJob recurringJob in _recurringJobs)
            {
                var recurringJobId = recurringJob.GetType().Name;
                var cronExpression = recurringJob.GetCronExpression();

                if (!string.IsNullOrEmpty(cronExpression))
                {
                    _recurringJobsManager.AddOrUpdate(
                        recurringJobId,
                        () => recurringJob.RunAsync(stoppingToken),
                        cronExpression);
                }
                else
                {
                    _recurringJobsManager.RemoveIfExists(recurringJobId);
                }
            }
        }
    }
}
