using System;
using System.Globalization;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Main.Views;
using OpenTK.Graphics.ES11;
using Serilog;

namespace Main.ViewModels;

public partial class UserViewModel : ViewModelBase, IContext
{
    [ObservableProperty]
    private string _username = "";
    [ObservableProperty]
    private string _password = "";
    
    private AppContext appContext;

    
    
    private readonly NavigationService _navigation;
    
    public UserViewModel(NavigationService navigation)
    {
        _navigation = navigation;
    }

    [RelayCommand]
    public void SwitchRegister()
    {
        GoToRegister();
    }


    [RelayCommand]
    public async Task LoginCommand()
    {
        bool ok = await Userhandling.ValidateLogin(Username, Password);

        if (ok)
        {
            Log.Information("Login successful");
            // ToDo: userdaten in Appcontext schreiben
            //appContext = new AppContext(user);
            
            // Vom AppContext den aktuellen Usernamen herausholen
            appContext.User.UserName = Username;
            
            //Navigieren zur nächsten Seite
            GoToFrontPage();
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

    public async Task UpdateContexts(AppContext appContext)
    {
        this.appContext = appContext;
    }

    private void GoToFrontPage()
    {
        _navigation.Navigate(Page.Front);
    }

    private void GoToRegister()
    {
        _navigation.Navigate(Page.Register);
    }
}