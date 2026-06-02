using System;
using System.ComponentModel;
using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using Main.Models;
using Main.Views;

namespace Main.ViewModels;

public partial class ShaderRendererViewModel : ViewModelBase
{
    [ObservableProperty] 
    private TextDocument _document;
    public ShaderRendererViewModel()
    {
        Document = new TextDocument();
    }
    
    public void SetContext(AppContext a, int shader_id)
    {
        Shader s = a.Shaders[shader_id];
        Document.Text = s.ShaderCode;
    }
}