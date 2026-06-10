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
    
    private AppContext AppContext { get; set; }

    // 
    public string CurrentUserName => AppContext?.CurrentUsername ?? "";

    [ObservableProperty]
    private bool _useFakeRepo= true; 
    private Uri baseadress = new("https://localhost:8000/");
    
    
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
        
        
        
        await InitContext(UseFakeRepo,baseadress);
        
        CurrentViewModel = UserViewModel;
        //CurrentViewModel = FrontPageViewModel;
        
    }
    
    
    public async Task InitContext(bool fake, Uri? ba)
    {
        // ToDo: User hier reinladen
        User user = new User(1, "auraman");
        
        if (!fake)
        {
            
            var handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            
            HttpClient client = new HttpClient(handler)
            {
                BaseAddress = ba
            }; 
            AppContext = new AppContext(user); 
            await AppContext.AsyncInit(client);
            
        }
        else
        {
            AppContext = new AppContext(user);
            await AppContext.FakeInit();
        }
        
        await FrontPageViewModel.UpdateContexts(AppContext);
        await UserViewModel.UpdateContexts(AppContext);
    }
    
    private void Navigate(Page page)
    {
        CurrentViewModel = page switch
        {
            Page.User => UserViewModel,
            Page.Front => FrontPageViewModel,
            Page.Register => RegisterViewModel,
            Page.Login => UserViewModel,
            _ => CurrentViewModel
        };
    }    
    private void NavigateId(int shaderId)
    {
        ShaderPageViewModel.UpdateContexts(AppContext, shaderId-1);
        
        CurrentViewModel = ShaderPageViewModel;
    }

    [RelayCommand]
    public async Task RemoveUser()
    {   
        // Der aktuelle User wird gelöscht.
        await Userhandling.DeleteUser(CurrentUserName);
        if (CurrentViewModel is FrontPageViewModel)
            CurrentViewModel = UserViewModel;
    }
    
    [RelayCommand]
    public void Swicheroo()
    {
        if (CurrentViewModel is FrontPageViewModel)
            CurrentViewModel = ShaderPageViewModel;
        else 
            CurrentViewModel = FrontPageViewModel;
        
        Log.Logger.Information("Page switched");
    }

    [RelayCommand]
    public void LogOut()
    {
        if (CurrentViewModel is FrontPageViewModel)
            CurrentViewModel = UserViewModel;
        else
            CurrentViewModel = FrontPageViewModel;

        Log.Logger.Information("Back to the Login");
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

}
public enum Page
{
    User,
    Front,
    Shader,
    Login,
    Register
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