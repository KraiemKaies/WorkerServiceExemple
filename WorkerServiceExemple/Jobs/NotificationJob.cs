using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace WorkerServiceExemple.Jobs
{
    class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
        public NotificationJob(ILogger<NotificationJob> logger)
        {
            this._logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Notify User at {DateTime.Now} and Jobtype: {context.JobDetail.JobType}");
            Console.WriteLine("Bonjour Kaies");
            return Task.CompletedTask;
        }
    }
}
