using ConsoleAppHangfire.Data;
using ConsoleAppHangfire.Jobs;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ConsoleAppHangfire
{
    public partial class Program
    {

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddData();
        }
            
        public static async Task Run()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));
            RecurringJob.AddOrUpdate<SaveClientJob>("Insert Client", x => x.Run(), Cron.Minutely);

            using (var server = new BackgroundJobServer())
            {
                Console.ReadLine();
            }
        }
    }
}
