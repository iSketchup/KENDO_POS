using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Serilog;

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
        bool ok = false;

        // NEU: Unterscheidung zwischen Admin und User
        if (IsAdminRegistration)
        {
            // Leitet die Anfrage an deinen Admin-Router im Python-Backend weiter
            ok = await AdminHandling.AddAdmin(Username, Password);
        }
        else
        {
            // Leitet die Anfrage an deinen normalen User-Router weiter
            ok = await Userhandling.AddUser(Username, Password);
        }

        if (ok)
        {
            Log.Information($"Register successful. IsAdmin: {IsAdminRegistration}");

            // Hier den AppContext mit dem Namen füllen
            appContext.User.UserName = Username;

            // Wenn du das IsAdmin Flag auch im C# Model hast, kannst du es hier gleich setzen:
            appContext.User.is_admin = IsAdminRegistration;

            GoToFrontPage();
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