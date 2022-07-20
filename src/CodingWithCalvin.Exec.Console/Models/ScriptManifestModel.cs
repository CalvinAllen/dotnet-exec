using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodingWithCalvin.Exec.Console.Models;

public class ScriptManifestModel
{
    [JsonPropertyName("scripts")] public Dictionary<string, string> Scripts { get; set; } = new();
}
