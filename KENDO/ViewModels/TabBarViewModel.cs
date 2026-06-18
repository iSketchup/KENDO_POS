using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Serilog;

namespace Main.ViewModels;

public partial class TabBarViewModel : ViewModelBase, IContext
{

    [ObservableProperty] private AppContext _appContext;
    
   
    private readonly NavigationService _navigation;

    public TabBarViewModel(NavigationService navigator)
    {
        _navigation = navigator;
    }

    [RelayCommand]
    public async Task RemoveUser()
    {
        // Der aktuelle User wird gelöscht.


            bool ok = await Userhandling.DeleteUser(AppContext.User.UserName);
            
            AppContext = new AppContext(new User());
            
            if (ok)
                _navigation.NavigateRequested(Page.Login);

        
    }


    [RelayCommand]
    public void GoToFrontPage()
    {
        _navigation.NavigateRequested(Page.Front);
    }


    [RelayCommand]
    public void GoToDashboard()
    {
        if (AppContext.User.IsAdmin)
        {
            _navigation.NavigateRequested(Page.Admin);
        }
    }


    [RelayCommand]
    public void LogOut()
    {
        AppContext.User = new User();

        _navigation.NavigateRequested(Page.Login);
        
    }


    [RelayCommand]
    public void ChangeUser()
    {
        _navigation.NavigateRequested(Page.Change);

    }

    [RelayCommand]
    public async Task CreateNewShader(string shaderName)
    {

        int newShaderId = await AppContext.CreateNewShader(shaderName);

        _navigation.NavigateRequestedId(newShaderId);
    }

    public Task UpdateContexts(AppContext appContext)
    {
        AppContext = appContext;
        return Task.CompletedTask;
    }
}