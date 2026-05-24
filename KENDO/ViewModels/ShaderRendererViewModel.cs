using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Main.ViewModels;

public partial class ShaderRendererViewModel : ViewModelBase
{   
    [ObservableProperty]
    private string _code;
    public void ChangeCode(string code)
    {
        // TODO: make this reload the shader itself
        Code = code;
    }
}