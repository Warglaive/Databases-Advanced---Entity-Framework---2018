using System;
using Banicharnica.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Banicharnica.App
{
    public class StartUp
    {
        public static void Main()
        {
            var service = ConfigureService();
        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<BanicharnicaContext>(opts => opts.UseSqlServer(Configuration.ConnectionString));
        }
    }
}