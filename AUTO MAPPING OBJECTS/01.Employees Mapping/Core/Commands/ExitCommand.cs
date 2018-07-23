using System;
using System.Threading;
using Banicharnica.App.Core.Contracts;

namespace Banicharnica.App.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            for (int i = 5; i > 0; i--)
            {
                Console.WriteLine($"Program closing in {i} seconds");
                Thread.Sleep(1000);
                if (i == 1)
                {
                    Console.WriteLine("Bye!");
                }
            }
            Environment.Exit(0);
            return null;
        }
    }
}