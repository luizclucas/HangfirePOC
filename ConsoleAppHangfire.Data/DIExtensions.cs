using ConsoleAppHangfire.Data.Repositories;
using ConsoleAppHangfire.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppHangfire.Data
{
    public static class DIExtensions
    {
        public static void AddData(this IServiceCollection services)
        {
            services.AddSingleton<DataFactory>();
            services.AddTransient<IClientRepository, ClientRepository>();
        }
    }
}
