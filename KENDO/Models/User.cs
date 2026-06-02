using System.Text.Json.Serialization;

namespace Main.Models;

public class User
{
    [JsonPropertyName("UserName")] // Dient dazu den username korrekt an den Server weiterzugeben
                // Das Json Property UserName wird für den Server auf die Alias geändert (UserName ...)
    public string? UserName { get; set; }
    public string? passwd { get; set; }
}