using Main.Models;
using Main.Views;

namespace Main.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ShaderRendererViewModel ShaderRenderer { get; } = new ();
    public CommentListViewModel Comments { get; } = new CommentListViewModel();
    
    public AppContext AppContext { get; }

    public MainWindowViewModel()
    {
        // ToDo: give this a user
        AppContext = new AppContext(null); 
        AppContext.CreatNewShader();
        
        ShaderRenderer.SetContext(AppContext);
    }
}