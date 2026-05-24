using System;

namespace Main.ViewModels;

public class ShaderRendererViewModel : ViewModelBase
{
    public string Code { get; set; } = "";
    public void ChangeCode(string code)
    {
        // TODO: make this reload the shader itself
        Code = code;
    }
}