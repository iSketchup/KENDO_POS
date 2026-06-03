using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace Main.ViewModels;

public class ShaderPageViewModel : ViewModelBase, IContext
{
    
    public ShaderRendererViewModel ShaderRenderer { get; } = new ();
    public CommentListViewModel Comments { get; } = new CommentListViewModel();

    public AppContext AppContext { get; private set; }

    public void UpdateContexts(AppContext appContext)
    {
        AppContext = appContext;
        
        ShaderRenderer.UpdateContexts(AppContext, 0);
        Comments.SetContext(AppContext, 0);
    }
}