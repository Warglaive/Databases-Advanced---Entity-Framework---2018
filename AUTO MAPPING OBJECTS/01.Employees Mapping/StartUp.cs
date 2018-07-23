using System;
using Banicharnica.App.Core;
using Banicharnica.Data;
using Banicharnica.Services;
using Banicharnica.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Banicharnica.App
{
    public class StartUp
    {
        public static void Main()
        {
            var service = ConfigureService();
            var engine = new Engine(service);
            engine.Run();
        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<BanicharnicaContext>(opts => opts.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<IDbInitizlizerService, DbInitizlizerService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}