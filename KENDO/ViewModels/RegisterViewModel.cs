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
    [ObservableProperty]
    private string? url;

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
    public async Task RegisterCommand()
    {
        if (!await ConnectToServer())
        {
            Log.Warning("Ungültige Server-URL");
            return;
        }



        bool ok = false;

        if (IsAdminRegistration)
        {
            ok = await AdminHandling.AddAdmin(Username, Password);
        }
        else
        {
            ok = await Userhandling.AddUser(Username, Password);
        }

        if (ok)
        {
            Log.Information($"Register successful. IsAdmin: {IsAdminRegistration}");

            appContext.User.UserName = Username;

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