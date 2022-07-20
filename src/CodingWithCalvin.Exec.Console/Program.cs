using CodingWithCalvin.Exec.Console.Commands;
using Spectre.Console.Cli;

namespace CodingWithCalvin.Exec.Console;

internal class Program 
{
    public static int Main(string[] args)
    {
        var app = new CommandApp<ExecCommand>();

        app.Configure(config =>
        {
            config.SetApplicationName("dotnet exec");
        });

        return app.Run(args);
    }
}
