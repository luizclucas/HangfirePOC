using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppHangfire
{
    public partial class Program
    {
        public static IServiceProvider RootServiceProvider;
        private static string _sqlConnection = "Server=localhost,1438;Initial Catalog=Hangfire;User Id=sa;Password=Carol146*";

        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.LiterateConsole()
                .WriteTo.Seq("http://seqserver.sicluster:5341", compact: true)
                .CreateLogger();

            try
            {
                var services = new ServiceCollection();
                ConfigureServices(services);
                var sp = services.BuildServiceProvider();
                RootServiceProvider = sp;

                var app = GetService<IApplicationBuilder>();
                app.UseHangfireDashboard("");

                GlobalConfiguration.Configuration
                           .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                           .UseColouredConsoleLogProvider()
                           .UseSimpleAssemblyNameTypeSerializer()
                           .UseRecommendedSerializerSettings()                           
                           .UseSqlServerStorage(_sqlConnection, new SqlServerStorageOptions
                           {
                               CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                               SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                               QueuePollInterval = TimeSpan.Zero,
                               UseRecommendedIsolationLevel = true,
                               UsePageLocksOnDequeue = true,
                               DisableGlobalLocks = true
                           });

                Log.Information("Running...");
                await Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled Exception");

                if (Debugger.IsAttached)
                    Debugger.Break();
            }
            finally
            {
                Log.Information("Press ENTER to exit...");
                Console.ReadLine();
            }
        }

            public static T GetService<T>() => RootServiceProvider.GetRequiredService<T>();
    }
}
