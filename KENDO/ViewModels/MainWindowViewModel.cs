using Main.Models;
using Main.Views;

namespace Main.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ShaderRendererViewModel ShaderRenderer { get; } 
    public CommentListViewModel Comments { get; } = new CommentListViewModel();

    public MainWindowViewModel()
    {
        ShaderRenderer =new( new Shader());
    }
}