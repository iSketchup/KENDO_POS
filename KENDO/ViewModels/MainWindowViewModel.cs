using Main.Models;
using Main.Views;

namespace Main.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ShaderRendererViewModel ShaderRenderer { get; } 

    public MainWindowViewModel()
    {
        ShaderRenderer =new( new Shader());
    }
}