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
    public FrontPageViewModel FrontPageViewModel { get; set; } 
    public ShaderPageViewModel ShaderPageViewModel { get; set; }
    
    private AppContext AppContext { get; set; }
    
    [ObservableProperty]
    private bool _useFakeRepo= true; 
    private Uri baseadress = new("http://localhost:8000/");

    public MainWindowViewModel()
    {
        Setup();
    }

    public async Task Setup()
    {
        
        ShaderPageViewModel = new ShaderPageViewModel();
        CurrentViewModel = ShaderPageViewModel;
        FrontPageViewModel = new FrontPageViewModel();
        
        
        await InitContext(UseFakeRepo,baseadress);
        
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Month, fileSizeLimitBytes: 1000000, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
            .CreateLogger();
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
            InitContext(UseFakeRepo,baseadress);
            Log.Logger.Information("Switched Repo Load status");
        }
        catch (HttpRequestException e)
        {
            Log.Logger.Error(e.Message); 
            UseFakeRepo =  true;
            
        }
    }

}