using Quartz;
using Quartz.Spi;
using System;

namespace WorkerServiceExemple.JobFactory
{
    public class MyJobFactory : IJobFactory
    {
        private readonly IServiceProvider _service;
        public MyJobFactory(IServiceProvider serviceProvider)
        {
            _service = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            return (IJob)_service.GetService(jobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {

        }
    }
}
