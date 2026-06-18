using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Main.Views;
using OpenTK.Audio.OpenAL;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Main.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ViewModelBase _currentViewModel;

    private FrontPageViewModel FrontPageViewModel { get; set; } 
    private ShaderPageViewModel ShaderPageViewModel { get; set; }
    private UserViewModel UserViewModel { get; set; }
    private RegisterViewModel RegisterViewModel { get; set; }
    private UserChangeViewModel UserChangeViewModel { get; set; } 
    private AdminViewModel AdminViewModel { get; set; }

    [ObservableProperty] private AppContext _appContext;

    // private string CurrentUserName => AppContext?.User.UserName ?? "";

    [ObservableProperty]
    private bool _useFakeRepo= false; 
    public static Uri baseadress = new("https://127.0.0.1:8000");
    
    private readonly NavigationService _navigation = new();
    

    public MainWindowViewModel()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
            .WriteTo.File(
                "log.txt",
                rollingInterval: RollingInterval.Month,
                fileSizeLimitBytes: 1000000,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
            .CreateLogger();
        
        Setup();
        
    }


    public async Task Setup()
    {
        
        _navigation.NavigateRequested = Navigate;
        _navigation.NavigateRequestedId = NavigateId;

        
        ShaderPageViewModel = new ShaderPageViewModel(_navigation);
        FrontPageViewModel = new FrontPageViewModel(_navigation);
        UserViewModel = new UserViewModel(_navigation);
        RegisterViewModel = new RegisterViewModel(_navigation);
        UserChangeViewModel = new UserChangeViewModel(_navigation);
        AdminViewModel = new AdminViewModel(_navigation);
        
        
        
        await InitContext(UseFakeRepo,baseadress);
        
        Log.Logger.Debug("switching to user ViewModel");
        CurrentViewModel = UserViewModel;
    }
    
    
    public async Task InitContext(bool fake, Uri? ba)
    {
        // ToDo: User hier reinladen
        if (!fake)
        {
            
            var handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            
            HttpClient client = new HttpClient(handler)
            {
                BaseAddress = ba
            }; 

            // Ein leerer User wird erstellt.
            AppContext = new AppContext(new User());  
            await AppContext.AsyncInit(client);
            
        }
        else
        {
            AppContext = new AppContext(new User());
            await AppContext.FakeInit();
        }
        
        await FrontPageViewModel.UpdateContexts(AppContext);
        await UserViewModel.UpdateContexts(AppContext);
        await UserChangeViewModel.UpdateContexts(AppContext);
        await RegisterViewModel.UpdateContexts(AppContext);
        await AdminViewModel.UpdateContexts(AppContext);
        
        Log.Logger.Information("fniishe init context");
    }

    
    private void Navigate(Page page)
    {
        CurrentViewModel = page switch
        {
            Page.User => UserViewModel,
            Page.Front => FrontPageViewModel,
            Page.Register => RegisterViewModel,
            Page.Login => UserViewModel,
            Page.Change => UserChangeViewModel,
            Page.Admin => AdminViewModel,
            _ => CurrentViewModel
        };
        
        if (page == Page.Admin)
        {
            // Startet das Laden im Hintergrund, sobald man auf die Admin-Seite wechselt
            _ = AdminViewModel.LoadAllUsers(); 
        }
    }   
    

    private async void NavigateId(int shaderId)
    {
        await ShaderPageViewModel.UpdateContexts(AppContext, shaderId);
        
        CurrentViewModel = ShaderPageViewModel;
    }


    [RelayCommand]
    public async Task RemoveUser()
    {
        // Der aktuelle User wird gelöscht.
        if (CurrentViewModel is UserChangeViewModel)
            CurrentViewModel = UserChangeViewModel;

        else if (CurrentViewModel is UserViewModel)
            CurrentViewModel = CurrentViewModel;

        else
        {

            bool ok = await Userhandling.DeleteUser(AppContext.User.UserName);


            if (ok)
                CurrentViewModel = UserViewModel;

        }
    }


    [RelayCommand]
    public void GoToFrontPage()
    {
        Navigate(Page.Front);
        FrontPageViewModel.UpdateContexts(AppContext);
        Log.Logger.Information("Page switched");
    }


    [RelayCommand]
    public void GoToDashboard()
    {
        if (AppContext.User.is_admin == true)
        {
            CurrentViewModel = AdminViewModel;
        }
    }


    [RelayCommand]
    public void LogOut()
    {
        if (CurrentViewModel is FrontPageViewModel)
        {
            AppContext.User.is_admin = false;
            CurrentViewModel = UserViewModel;
        }
        else if (CurrentViewModel is UserViewModel)
        {
            AppContext.User.is_admin = false;
            CurrentViewModel = UserViewModel;
        }
            
        else if (CurrentViewModel is RegisterViewModel)
        {
            AppContext.User.is_admin = false;
            CurrentViewModel = RegisterViewModel;
        }
        else
        {
            AppContext.User.is_admin = false;
            CurrentViewModel = UserViewModel;
        }
            


        Log.Logger.Information("Back to the Login");
    }


    [RelayCommand]
    public void ChangeUser()
    {
        if (CurrentViewModel is FrontPageViewModel)
            CurrentViewModel = UserChangeViewModel;
        else if (CurrentViewModel is UserViewModel)
            CurrentViewModel = UserViewModel;
        else if (CurrentViewModel is RegisterViewModel)
            CurrentViewModel = RegisterViewModel;
        else
            CurrentViewModel = UserChangeViewModel;


        Log.Logger.Information("To the Userdata ChangeWindow");
    }

    [RelayCommand]
    public async void SwitchLoaded()
    {
        try
        {
            await InitContext(UseFakeRepo,baseadress);
            Log.Logger.Information("Switched Repo Load status");
        }
        catch (HttpRequestException e)
        {
            Log.Logger.Error(e.Message); 
            UseFakeRepo =  true;
            
        }
    }

    [RelayCommand]
    public async Task CreateNewShader(string shaderName)
    {
        
        int newShaderId = await AppContext.CreateNewShader(shaderName);
        
        FrontPageViewModel.UpdateContexts(AppContext);
        NavigateId(newShaderId);
    }

}


public enum Page
{
    User,
    Front,
    Login,
    Register,
    Change,
    Admin
}


public class NavigationService
{
    public Action<Page>? NavigateRequested { get; set; }
    public Action<int>? NavigateRequestedId { get; set; }

    public void Navigate(Page page)
    {
        NavigateRequested?.Invoke(page);
    }    
    public void Navigate(int shaderId)
    {
        NavigateRequestedId?.Invoke(shaderId);
    }
}