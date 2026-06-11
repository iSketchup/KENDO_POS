using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;

namespace Main.ViewModels;

public partial class UserChangeViewModel: ViewModelBase
{
    [ObservableProperty]
    private string _username = "";
    [ObservableProperty]
    private string _password = "";
    
    private AppContext appContext;

    
    
    private readonly NavigationService _navigation;
    
    public UserChangeViewModel(NavigationService navigation)
    {
        _navigation = navigation;
    }

    [RelayCommand]
    public async Task ChangeUser()
    {
        bool ok = await Userhandling.ChangeUser(appContext.User.UserName, Username, Password);
        
        if (ok)
        {
            Log.Information("Userdata was changed");

            appContext.User.UserName = Username;
            appContext.User.passwd = Password;

            Homepage();
        }
        else
        {
            Log.Warning("Userdata could not be changed");
        }
        
        Log.Logger.Information("ChangeUserCommand");
    }
    
    public async Task UpdateContexts(AppContext appContext)
    {
        this.appContext = appContext;
    }
    
    [RelayCommand]
    public void Homepage()
    {
        _navigation.Navigate(Page.Front);
    }
}