using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Serilog;

namespace Main.ViewModels;

public partial class RegisterViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _username = "";
    [ObservableProperty]
    private string _password = "";
    
    private AppContext appContext;
    private readonly NavigationService _navigation;


    [RelayCommand]
    public void SwitchLogin()
    {
        GoLogin();
    }

    [RelayCommand]
    public async Task RegisterCommand()
    {
        bool ok = await Userhandling.AddUser(Username, Password);
        
        if (ok)
        {
            Log.Information("Register successful");
            // ToDo: userdaten in Appcontext schreiben
            //appContext = new AppContext(user);
            
            //Navigieren zur nächsten Seite
            GoToFrontPage();
        }
        else
        {
            Log.Warning("Register failed");
            // TODO: Fehlermeldung anzeigen.
            
        }
        Log.Logger.Information("LoginCommand");
    }
    
    public RegisterViewModel(NavigationService navigation)
    {
        _navigation = navigation;
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