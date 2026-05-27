using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Main.Models;

namespace Main.ViewModels;

public partial class ShaderRendererViewModel : ObservableObject
{
    [ObservableProperty] 
    private Shader _context; 



    public ShaderRendererViewModel(Shader shader)
    {
        Context = shader;
    }
}