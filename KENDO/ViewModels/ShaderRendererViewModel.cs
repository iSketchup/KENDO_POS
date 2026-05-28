using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Main.Models;

namespace Main.ViewModels;

public partial class ShaderRendererViewModel : ViewModelBase
{
    [ObservableProperty] 
    private Shader _context; 

    public void setShader(Shader s)
    {
        Context = s;
    }
}