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