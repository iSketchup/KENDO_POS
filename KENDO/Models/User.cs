using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Main.Models;

public partial class User : ObservableObject
{
    public int Id { get; set; }

    [property: JsonPropertyName("UserName")]
    [ObservableProperty]
    private string? _userName;

    [ObservableProperty]
    private string? _passwd;


    [JsonPropertyName("is_admin")]
    public bool is_admin { get; set; }

    public object IsAdmin { get; }

    public User() { } // ToDo: fix ts, Temp fix for users not being loaded

    public User(int id, string? userName)
    {
        Id = id;
        UserName = userName;
    }

    public override string ToString()
    {
        return UserName;
    }
}