using System;
using System.ComponentModel;
using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Main.Views;
using Serilog;

namespace Main.ViewModels;

public partial class ShaderRendererViewModel : ViewModelBase
{
    [ObservableProperty] 
    private TextDocument _document;

    [ObservableProperty] private Shader _shader;
    
    
    
    private readonly NavigationService _navigation;
    
    public ShaderRendererViewModel(NavigationService navigation)
    {
        Document = new TextDocument();
        _navigation = navigation;
    }
    
    public void UpdateContexts(AppContext a, int shader_id)
    {
        Log.Logger.Debug("Updating shader context for " + shader_id);
        
        Shader = a.Shaders[shader_id];
        
        Document.Text = Shader.ShaderCode;
    }
    
    [RelayCommand]
    public void GoToThisShader()
    {
        int id = Shader.ShaderId;

        _navigation.NavigateRequestedId(id);
        
        Log.Logger.Information("Opened shader: "+id );
    }
}