#! "netcoreapp2.1"
#r "nuget: McMaster.Extensions.CommandLineUtils, 2.2.5"
#load "consoleController.csx"

using McMaster.Extensions.CommandLineUtils;

var consoleController = new ConsoleController();
var app = new CommandLineApplication(throwOnUnexpectedArg: false);

app.Command("add", c => {
    c.OnExecute(() => {
        consoleController.AddCustomer();
        return 0;
    });
});

app.Command("search", c => {
    c.OnExecute(() => {
        consoleController.SearchForRepresentative();
        return 0;
    });
});

app.OnExecute(() => {
    WriteHelpText();
    return 0;
});

void WriteHelpText() {
    Console.WriteLine("Add new customers [add] or search for a customer representative [search] by customer");
}

void StartCommandLoop() {
    while (true) {
        var command = Console.ReadLine();
        app.Execute(command);
    }
}

WriteHelpText();

StartCommandLoop();