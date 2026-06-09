using System.Text.Json.Serialization;

namespace Main.Models;

public class User
{
    
    public int Id { get; set; }
    [JsonPropertyName("UserName")] // Dient dazu den username korrekt an den Server weiterzugeben
                // Das Json Property UserName wird für den Server auf die Alias geändert (UserName ...)
    public string? UserName { get; set; }
    public string? passwd { get; set; } // ToDo: Luis erklären warum das nötig ist

    public User() { } // ToDo: fix ts, Temp fix for users not being loaded

    public User(int id, string? userName)
    {
        Id = id;
        UserName = userName;
    }
}