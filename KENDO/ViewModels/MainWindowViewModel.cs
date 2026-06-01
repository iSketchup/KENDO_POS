using System;
using System.Net.Http;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Main.Views;

namespace Main.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ViewModelBase _currentViewModel;
    public FrontPageViewModel FrontPageViewModel { get; set; }
    public ShaderPageViewModel ShaderPageViewModel { get; set; }

    public MainWindowViewModel()
    {
        ShaderPageViewModel = new ShaderPageViewModel();
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

        Console.WriteLine("ARSCHLOCH");
    }

}