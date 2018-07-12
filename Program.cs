using System;
using McMaster.Extensions.CommandLineUtils;

namespace value_object_sample
{
    class Program
    {
        private readonly ConsoleController _consoleController = new ConsoleController();

        static void Main(string[] args)
        {
            new Program().Start();
        }

        void Start()
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false);

            app.Command("add", c =>
            {
                c.OnExecute(() =>
                {
                    _consoleController.AddCustomerAction();
                    return 0;
                });
            });

            app.Command("search", c =>
            {
                c.OnExecute(() =>
                {
                    _consoleController.SearchForRepresentativeAction();
                    return 0;
                });
            });

            app.OnExecute(() =>
            {
                WriteHelpText();
                return 0;
            });
            
            WriteHelpText();

            while (true)
            {
                var command = Console.ReadLine();
                app.Execute(command);
            }

            // ReSharper disable once FunctionNeverReturns
        }

        void WriteHelpText()
        {
            Console.WriteLine("Add new customers [add] or search for a customer representative [search] by customer");
        }
    }
}