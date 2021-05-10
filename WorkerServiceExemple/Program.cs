using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using WorkerServiceExemple.JobFactory;
using WorkerServiceExemple.Jobs;
using WorkerServiceExemple.Models;
using WorkerServiceExemple.Schedular;

namespace WorkerServiceExemple
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //    services.AddHostedService<Worker>();
                    //    services.AddHostedService<MyService>();

                    services.AddSingleton<IJobFactory, MyJobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                    services.AddSingleton<NotificationJob>();
            
                    services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notify First Job", "0/5 * * * * ?"));
                    services.AddHostedService<MySchedular>();
                    
                });
    }
}
