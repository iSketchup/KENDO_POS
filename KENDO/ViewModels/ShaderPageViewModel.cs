using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace Main.ViewModels;

public class ShaderPageViewModel : ViewModelBase
{
    
    public ShaderRendererViewModel ShaderRenderer { get; } = new ();
    public CommentListViewModel Comments { get; } = new CommentListViewModel();

    public AppContext AppContext { get; private set; }
    public async Task InitContext(bool fake, Uri? ba)
    {
        
        if (!fake)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = ba
            }; 
            AppContext = new AppContext(null); 
            await AppContext.AsyncInit(client);
        }
        else
        {
            AppContext = new AppContext(null);
            AppContext.FakeInit();
        }
        
        ShaderRenderer.SetContext(AppContext, 0);
        Comments.SetContext(AppContext, 0);
    }

}