using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System.Threading;
using System.Threading.Tasks;
using WorkerServiceExemple.Models;

namespace WorkerServiceExemple.Schedular
{
    class MySchedular : IHostedService
    {
        public IScheduler Scheduler { get; set; }
        private readonly IJobFactory _jobFactory;
        private readonly JobMetadata _jobMetadata;
        private readonly ISchedulerFactory _schedulerFactory;

        public MySchedular(ISchedulerFactory schedulerFactory, JobMetadata jobMetadata, IJobFactory jobFactory)
        {
            this._jobFactory = jobFactory;
            this._schedulerFactory = schedulerFactory;
            this._jobMetadata = jobMetadata;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //Creating Schdeular
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            //Create Job
            IJobDetail jobDetail = CreateJob(_jobMetadata);

            //Create trigger
            ITrigger trigger = CreateTrigger(_jobMetadata);

            //Schedule Job
            await Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);

            //Start The Schedular
            await Scheduler.Start(cancellationToken);
        }

        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobMetadata.JobId.ToString())
                .WithCronSchedule(jobMetadata.CronExpression)
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder.Create(jobMetadata.JobType)
                .WithIdentity(jobMetadata.JobId.ToString())
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler.Shutdown(cancellationToken);
        }
    }
}
