using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
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

    [ObservableProperty] private ObservableCollection<ImageDropSlotViewModel> _imageDropSlots = new() { new() };
    
    
    private readonly NavigationService _navigation;
    
    public ShaderRendererViewModel(NavigationService navigation)
    {
        Document = new TextDocument();
        _navigation = navigation;
    }
    
    public void UpdateContexts(Shader shader)
    {
        Shader = shader;
        
        Log.Logger.Debug("Updating shader context for " + Shader.ShaderId);

        
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