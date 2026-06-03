using System;
using System.Globalization;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Serilog;

namespace Main.ViewModels;

public partial class UserViewModel : ViewModelBase
{
    private string _username = "";
    private string _password = "";

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }
    
    public IRelayCommand LoginCommand { get; }

    public UserViewModel()
    {
        LoginCommand = new AsyncRelayCommand(DoLogin);
    }

    private async Task DoLogin()
    {
        User? user = await Userhandling.ValidateLogin(Username, Password);

        if (user != null)
        {
            Log.Information("Login successful");
            // TODO: Navigieren zur nächsten Seite
            
        }
        else
        {
            Log.Warning("Login failed");
            // TODO: Fehlermeldung anzeigen.
            
        }
        Log.Logger.Information("LoginCommand");
    }


    public void LoginUser(User user)
    {
        
    }
}