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
    [ObservableProperty]
    private string? url;

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



    private async Task<bool> ConnectToServer()
    {
        if (!Uri.TryCreate(Url, UriKind.Absolute, out var uri))
            return false;

        var handler = new HttpClientHandler();

        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient client = new HttpClient(handler)
        {
            BaseAddress = uri
        };

        Userhandling.SetApiService(new ApiService(client));

        appContext = new AppContext(new User());
        await appContext.AsyncInit(client);

        return true;
    }


    [RelayCommand]
    public async Task LoginCommand()
    {
        if (!await ConnectToServer())
        {
            Log.Warning("Ungültige Server-URL");
            return;
        }


        User? loggedInUser = await Userhandling.ValidateLogin(Username, Password);

        if (loggedInUser.UserName != null && loggedInUser.passwd != null)
        {
            Log.Information($"Login successful for {loggedInUser.UserName}");

            //appContext.User = loggedInUser;
            appContext.User.UserName = loggedInUser.UserName;
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