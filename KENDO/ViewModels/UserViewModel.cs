using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Main.Views;
using OpenTK.Graphics.ES11;
using Serilog;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Main.ViewModels;

public partial class UserViewModel : ViewModelBase, IContext
{
    [ObservableProperty]
    private string _errorMessage = "";
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
        ErrorMessage = "";

        if (string.IsNullOrEmpty(Username) || Username.Contains(" "))
        {
            ErrorMessage = "username must not be empty or contain any whitespaces";
            Log.Warning("Register failed: Invalid Username");
            return;
        }


        if (string.IsNullOrEmpty(Password) || Password.Contains(" "))
        {
            ErrorMessage = "password must not be empty or contain any whitespaces";
            Log.Warning("Register failed: Password contains spaces");
            return;
        }


        if (Password.Length < 8)
        {
            ErrorMessage = "Password must be 8 characters long.";
            Log.Warning("Register failed: Password too short");
            return;
        }

        User? loggedInUser = await Userhandling.ValidateLogin(Username, Password);

        if (loggedInUser.UserName != null && loggedInUser.Passwd != null)
        {
            Log.Information($"Login successful for {loggedInUser.UserName}");

            appContext.SetUser(loggedInUser);
            
            
            if (appContext.User.IsAdmin)
            {
                Log.Information("User is Admin. Navigating to AdminView...");
                _navigation.Navigate(Page.Admin);
            }
            else
            {
                Log.Information("User is Standard User. Navigating to FrontPage...");
                _navigation.Navigate(Page.Front);
            }
        }
        else
        {
            ErrorMessage = "Login failed: wrong username or password";
            Log.Warning("Login failed: Wrong username or password");
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