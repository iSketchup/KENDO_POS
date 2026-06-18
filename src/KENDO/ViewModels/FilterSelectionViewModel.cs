using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Main.Models;

namespace Main.ViewModels;

public partial class FilterSelectionViewModel : ViewModelBase
{
    AppContext appContext;
    public List<Shader> Shaders;
    public string? shaderUserName { get; set; } = null;
    public string? shaderName { get; set; } = null;
    public List<string> tags { get; set; } 
    
    public FrontPageViewModel FpViewModel;
    
    

    [RelayCommand]
    public async void SetFilterParameters()
    {
        FpViewModel.UpdateContexts(appContext, shaderUserName, shaderName, tags);
    }

    public void SetContext(AppContext a, List<Shader> shaders, FrontPageViewModel fp)
    {
        appContext = a;
        Shaders = shaders;
        FpViewModel = fp;
    }
    
}