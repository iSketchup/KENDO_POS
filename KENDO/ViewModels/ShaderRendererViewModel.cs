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
    [ObservableProperty] 
    private Shader _shader;
    
    public ShaderRendererViewModel()
    {
        Document = new TextDocument();
    }
    
    public void UpdateContexts(AppContext a, int shader_id)
    {
        Log.Logger.Debug("Updating shader context for " + shader_id);
        
        Shader = a.Shaders[shader_id];
        
        Document.Text = Shader.ShaderCode;
    }
    
    [RelayCommand]
    public void Opened()
    {
        Log.Logger.Information("Opened shader: " + Shader.ShaderId);
    }
}