using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Main.ViewModels;

public partial class RegisterViewModel : ViewModelBase, IContext
{
    [ObservableProperty]
    private string _errorMessage = "";

    [ObservableProperty]
    private string _username = "";

    [ObservableProperty]
    private string _password = "";

    // NEU: Eigenschaft für die Checkbox im UI
    [ObservableProperty]
    private bool _isAdminRegistration;

    private AppContext appContext;
    private readonly NavigationService _navigation;

    public RegisterViewModel(NavigationService navigation)
    {
        _navigation = navigation;
    }

    [RelayCommand]
    public void SwitchLogin()
    {
        GoLogin();
    }



    [RelayCommand]
    public async Task RegisterCommand()
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


        // (Umgeht das Problem, dass man sich noch separat anmelden muss)
        User? sign_user = new User();


        sign_user = await Userhandling.AddUser(Username, Password);

        if (sign_user != null)
        {
            Log.Information($"Register successful. IsAdmin: {IsAdminRegistration}");

            appContext.User.UserName = Username;
            appContext.User.Id = sign_user.Id;

            appContext.User.IsAdmin = IsAdminRegistration;
            
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
            ErrorMessage = "Registration failed. This username is already used.";
            Log.Warning("Register failed");
            // TODO: Fehlermeldung in der UI anzeigen (z.B. über ein weiteres ObservableProperty)
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

    private void GoLogin()
    {
        _navigation.Navigate(Page.Login);
    }
}