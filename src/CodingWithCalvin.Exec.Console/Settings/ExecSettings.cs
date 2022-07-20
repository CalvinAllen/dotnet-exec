using Spectre.Console.Cli;

namespace CodingWithCalvin.Exec.Console.Settings;

public class ExecSettings : CommandSettings
{
    [CommandArgument(0, "[ScriptName]")]
    public string? ScriptName { get; set; }
}
