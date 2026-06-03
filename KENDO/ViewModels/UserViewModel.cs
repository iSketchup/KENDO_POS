using System;
using System.Globalization;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Serilog;

namespace Main.ViewModels;

public partial class UserViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _username = "";
    [ObservableProperty]
    private string _password = "";


    [RelayCommand]
    private async Task LoginCommand()
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