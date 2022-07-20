using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CodingWithCalvin.Exec.Console.Models;
using CodingWithCalvin.Exec.Console.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CodingWithCalvin.Exec.Console.Commands;

public class ExecCommand : AsyncCommand<ExecSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, ExecSettings settings)
    {
        var scriptFile = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), ".dotnet"));
        var scriptManifest = JsonSerializer.Deserialize<ScriptManifestModel>(scriptFile);

        if (scriptManifest is null)
        {
            AnsiConsole.Write(new Markup("[red]Unable to locate .dotnet script manifest file[/]"));
            return -1;
        }

        if (string.IsNullOrEmpty(settings.ScriptName))
        {
            AnsiConsole.Write(new Markup("[red]`ScriptName` is a required parameter[/]"));
            return -1;
        }
        
        if (!scriptManifest.Scripts.ContainsKey(settings.ScriptName))
        {
            AnsiConsole.Write(new Markup($"[red]Unable to locate a script that matches '{settings.ScriptName}'[/]"));
            return -1;
        }
        
        using (var process = new Process())
        {
            var command = scriptManifest.Scripts[settings.ScriptName!];
            process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = string.Concat("/d /s /c \"", command, "\"");

            process.Start();

            await process.WaitForExitAsync(CancellationToken.None);

            return process.ExitCode;
        }
    }
}

