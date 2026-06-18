using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Main.Models;

public partial class User : ObservableObject
{
    [property: JsonPropertyName("UserId")]
    [ObservableProperty]
    private int _id;
    //public int Id { get; set; }

    [property: JsonPropertyName("UserName")]
    [ObservableProperty]
    private string? _userName;

    [ObservableProperty]
    [property: JsonPropertyName("passwd")]
    private string? _passwd;


    [ObservableProperty] [JsonPropertyName("is_admin")]
    private bool _isAdmin;


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