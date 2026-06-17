using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Main.Models;

public partial class User : ObservableObject
{
    public int Id { get; set; }

    [NotifyPropertyChangedFor(nameof(LoggedIn))]
    [property: JsonPropertyName("UserName")]
    [ObservableProperty]
    private string? _userName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LoggedIn))]
    private string? _passwd;

    public bool LoggedIn =>
        !string.IsNullOrWhiteSpace(UserName) &&
        !string.IsNullOrWhiteSpace(Passwd);

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