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

    public User User { get; private set; }

    private FrontPageViewModel FrontPageViewModel { get; set; } 
    private ShaderPageViewModel ShaderPageViewModel { get; set; }
    private UserViewModel UserViewModel { get; set; }
    private RegisterViewModel RegisterViewModel { get; set; }
    private UserChangeViewModel UserChangeViewModel { get; set; } 
    private AdminViewModel AdminViewModel { get; set; }
    private UrlPickerViewModel UrlPickerViewModel { get; set; }

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
        UrlPickerViewModel = new UrlPickerViewModel(_navigation);
        
        
        
        
        Log.Logger.Debug("switching to user ViewModel");
        CurrentViewModel = UrlPickerViewModel;
    }

    public async void SetContext()
    {
        await FrontPageViewModel.UpdateContexts(AppContext);
        await UserViewModel.UpdateContexts(AppContext);
        await UserChangeViewModel.UpdateContexts(AppContext);
        await RegisterViewModel.UpdateContexts(AppContext);
        await AdminViewModel.UpdateContexts(AppContext);

    }
    
    public static async Task<AppContext> InitContext(bool fake, User? currentUser)
    {
        // ToDo: User hier reinladen

        AppContext appContext;
        if (!fake)
        {
            
            var handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            
            HttpClient client = new HttpClient(handler)
            {
                BaseAddress = baseadress
            };

            // Ein leerer User wird erstellt.
            appContext = new AppContext(currentUser);
            await appContext.AsyncInit(client);
            
        }
        else
        {
            appContext = new AppContext(currentUser);
            await appContext.FakeInit();
        }
        
        return appContext;
    }

    
    private async void Navigate(Page page)
    {
        User? currentUser = AppContext?.User;

       AppContext =  await InitContext(UseFakeRepo, currentUser);
       SetContext();
       
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