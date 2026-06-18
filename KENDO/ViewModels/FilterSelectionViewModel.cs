using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Main.Models;

namespace Main.ViewModels;

public partial class FilterSelectionViewModel : ViewModelBase
{
    AppContext appContext;
    public List<Shader> Shaders;
    public string? shaderUserName = null;
    public string? shaderName = null;
    public List<string> tags = null;
    public FrontPageViewModel FpViewModel;
    
    

    [RelayCommand]
    public async void SetFilterParameters(TextBox? shaderNameTextBox)
    {
        if (shaderNameTextBox is null)
            return;
        this.shaderUserName = shaderNameTextBox.Text;
        this.shaderName = shaderName;
        this.tags = tags;
        
        //Shaders = await appContext.GetShadersByFilter(shaderNameTextBox.Text);
        FpViewModel.UpdateContexts(appContext, shaderUserName, shaderName, tags);
        
        shaderNameTextBox.Clear();
    }

    public void SetContext(AppContext a, List<Shader> shaders, FrontPageViewModel fp)
    {
        appContext = a;
        Shaders = shaders;
        FpViewModel = fp;
    }
    
}