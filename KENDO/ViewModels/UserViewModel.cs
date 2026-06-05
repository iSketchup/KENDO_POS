using System;
using System.Globalization;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
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
    public async Task LoginCommand()
    {
        User? user = await Userhandling.ValidateLogin(Username, Password);

        if (user != null)
        {
            Log.Information("Login successful");
            // ToDo: userdaten in Appcontext schreiben
            appContext = new AppContext(user);
            
            //Navigieren zur nächsten Seite
            GoToFrontPage();

        }
        else
        {
            Log.Warning("Login failed");
            // TODO: Fehlermeldung anzeigen.
            
        }
        Log.Logger.Information("LoginCommand");
    }


    public void LoginUser(User user)
    {
        
    }

    public void UpdateContexts(AppContext appContext)
    {
        this.appContext = appContext;
    }

    private void GoToFrontPage()
    {
        _navigation.Navigate(Page.Front);
    }
}