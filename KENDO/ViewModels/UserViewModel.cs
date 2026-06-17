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

        User? loggedInUser = await Userhandling.ValidateLogin(Username, Password);

        if (loggedInUser.UserName != null && loggedInUser.Passwd != null)
        {
            Log.Information($"Login successful for {loggedInUser.UserName}");

            //appContext.User = loggedInUser;
            appContext.User.UserName = loggedInUser.UserName;
            appContext.User.Id = loggedInUser.Id;
            appContext.User.is_admin = loggedInUser.is_admin;
            
            
            if (appContext.User.is_admin)
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