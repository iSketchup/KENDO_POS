using System;
using System.Net.Http;
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
    private bool UseFakeRepo= true; 
    private Uri baseadress = new("http://localhost:8000/");

    public MainWindowViewModel()
    {
        ShaderPageViewModel = new ShaderPageViewModel();
        ShaderPageViewModel.InitContext(UseFakeRepo,baseadress);
        
        FrontPageViewModel = new FrontPageViewModel();
        CurrentViewModel = ShaderPageViewModel;
        
        
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Month, fileSizeLimitBytes: 1000000, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
            .CreateLogger();
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
    public void SwitchLoaded()
    {
        if (UseFakeRepo)
        {
            UseFakeRepo = false;
        }
        else
        {
            UseFakeRepo = true;
        }
        ShaderPageViewModel.InitContext(UseFakeRepo,baseadress);
        Log.Logger.Information("Switched Repo Load status");
    }

}