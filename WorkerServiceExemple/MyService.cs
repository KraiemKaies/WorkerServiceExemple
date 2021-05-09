using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceExemple
{
    class MyService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Worker is running at: { DateTime.Now}");
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
