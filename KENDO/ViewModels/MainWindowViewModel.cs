using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Main.Views;
using Serilog;

namespace Main.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ViewModelBase _currentViewModel;

    private FrontPageViewModel FrontPageViewModel { get; set; } 
    private ShaderPageViewModel ShaderPageViewModel { get; set; }
    private UserViewModel UserViewModel { get; set; }
    
    private AppContext AppContext { get; set; }
    
    [ObservableProperty]
    private bool _useFakeRepo= true; 
    private Uri baseadress = new("http://localhost:8000/");

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
        
        ShaderPageViewModel = new ShaderPageViewModel();
        FrontPageViewModel = new FrontPageViewModel();
        UserViewModel = new UserViewModel();
        
        CurrentViewModel = UserViewModel;
        
        
        await InitContext(UseFakeRepo,baseadress);
        
    }
    
    
    public async Task InitContext(bool fake, Uri? ba)
    {
        
        if (!fake)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = ba
            }; 
            AppContext = new AppContext(null); 
            await AppContext.AsyncInit(client);
        }
        else
        {
            AppContext = new AppContext(null);
            await AppContext.FakeInit();
        }
        ShaderPageViewModel.UpdateContexts(AppContext);
        FrontPageViewModel.UpdateContexts(AppContext);
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