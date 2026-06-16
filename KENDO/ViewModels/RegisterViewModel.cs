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