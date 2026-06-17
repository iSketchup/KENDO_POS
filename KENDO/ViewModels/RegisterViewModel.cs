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
        // Falls man einen Admin erstellen möchte
        // (Umgeht das Problem, dass man sich noch separat anmelden muss)
        User? sign_user = new User();


        if (IsAdminRegistration)
        {
            sign_user = await AdminHandling.AddAdmin(Username, Password);
        }
        else
        {
            sign_user = await Userhandling.AddUser(Username, Password);
        }

        if (sign_user != null)
        {
            Log.Information($"Register successful. IsAdmin: {IsAdminRegistration}");

            appContext.User.UserName = Username;
            appContext.User.Id = sign_user.Id;

            appContext.User.is_admin = IsAdminRegistration;
            
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