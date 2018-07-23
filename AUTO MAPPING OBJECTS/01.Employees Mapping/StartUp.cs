using System;
using AutoMapper;
using Banicharnica.App.Core;
using Banicharnica.App.Core.Contracts;
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

            serviceCollection.AddAutoMapper(x => x.AddProfile<BanicharnicaProfile>());

            serviceCollection.AddTransient<IDbInitizlizerService, DbInitizlizerService>();

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();

            serviceCollection.AddTransient<IEmployeeController, IEmployeeController>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}