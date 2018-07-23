using System;
using Banicharnica.App.Core.Contracts;
using Banicharnica.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Banicharnica.App.Core
{
    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;
        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Run()
        {
            var initializeDb = this.serviceProvider.GetService<IDbInitizlizerService>();
            initializeDb.InitializeDatabase();

            var commandInterpreter = this.serviceProvider.GetService<ICommandInterpreter>();

            while (true)
            {
                var input = Console.ReadLine().Split(" "
                    , StringSplitOptions.RemoveEmptyEntries);

                var result = commandInterpreter.Read(input);
                Console.WriteLine(result);
            }
        }
    }
}