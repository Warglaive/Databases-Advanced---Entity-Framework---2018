using System;
using System.Linq;
using System.Reflection;
using Banicharnica.App.Core.Contracts;

namespace Banicharnica.App.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IServiceProvider serviceProvider;
        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public string Read(string[] input)
        {
            var commandName = input[0] + "Command";
            var args = input.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly().GetTypes()
                .FirstOrDefault(x => x.Name == commandName);
            var constructor = type.GetConstructors().First();

            var consructorParameters = constructor.GetParameters().Select(x => x.ParameterType);

            var service = consructorParameters.Select(this.serviceProvider.GetService).ToArray();

            var command = (ICommand)constructor.Invoke(service);
            var result = command.Execute(args);
            return result;
        }
    }
}