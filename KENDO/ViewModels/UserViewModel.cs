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
        // 1. Login anfordern und User-Objekt erhalten
        User? loggedInUser = await Userhandling.ValidateLogin(Username, Password);

        if (loggedInUser != null)
        {
            Log.Information($"Login successful for {loggedInUser.UserName}");

            // 2. Daten in den globalen AppContext schreiben
            appContext.User.UserName = loggedInUser.UserName;
            appContext.User.IsAdmin = loggedInUser.IsAdmin;
            appContext.User.Id = loggedInUser.Id;

            // 3. Rollenprüfung: Wo soll der User hin?
            if (appContext.User.IsAdmin)
            {
                Log.Information("User is Admin. Navigating to AdminView...");
                _navigation.Navigate(Page.Admin); // Registriere Page.Admin in deinem NavigationService
            }
            else
            {
                Log.Information("User is Standard User. Navigating to FrontPage...");
                _navigation.Navigate(Page.Front);
            }
        }
        else
        {
            Log.Warning("Login failed: Wrong username or password");
            // ToDo: Fehlermeldung in der UI anzeigen
        }
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