using Banicharnica.App.Core.Contracts;

namespace Banicharnica.App.Core.Commands
{
    public class SetAddressCommand : ICommandInterpreter
    {
        private const string SucessMessage = "Sucess!";
        private readonly IEmployeeController controller;
        public SetAddressCommand(IEmployeeController controller)
        {
            this.controller = controller;
        }

        public string Read(string[] input)
        {
            var id = int.Parse(input[0]);
            var address = input[1];
            this.controller.SetAddress(id, address);
            return SucessMessage;
        }
    }
}