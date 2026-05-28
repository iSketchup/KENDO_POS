using System;
using System.ComponentModel;
using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using Main.Models;

namespace Main.ViewModels;

public partial class ShaderRendererViewModel : ViewModelBase
{
    [ObservableProperty] 
    private TextDocument _document;
        public ShaderRendererViewModel()
    {
        Document = new TextDocument();
    }
    
    public void SetShader(Shader s)
    {
        Document.Text = s.Code;
    }
}