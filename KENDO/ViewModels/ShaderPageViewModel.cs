using System;
using System.Net.Http;
using CommunityToolkit.Mvvm.Input;

namespace Main.ViewModels;

public class ShaderPageViewModel : ViewModelBase
{
    
    public ShaderRendererViewModel ShaderRenderer { get; } = new ();
    public CommentListViewModel Comments { get; } = new CommentListViewModel();
    
    public AppContext AppContext { get; }

    public ShaderPageViewModel()
    {
        
        bool Fake = true;
        Uri Ba = null;

        if (!Fake)
        {


            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8000/")
            }; 
            AppContext = new AppContext(null, client); 
        }
        else
        {
            AppContext = new AppContext(null, null);
        }

        
        ShaderRenderer.SetContext(AppContext, 0);
        Comments.SetContext(AppContext, 0);
    }

}