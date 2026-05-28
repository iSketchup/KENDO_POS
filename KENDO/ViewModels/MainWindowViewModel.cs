using Main.Models;
using Main.Views;

namespace Main.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ShaderRendererViewModel ShaderRenderer { get; } = new ();
    public CommentListViewModel Comments { get; } = new CommentListViewModel();

    public MainWindowViewModel()
    {
        ShaderRenderer.SetShader(new Shader());
    }
}