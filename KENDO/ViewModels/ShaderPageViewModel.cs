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
       
        HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:8000/")
        }; 
        
        // ToDo: give this a user
        AppContext = new AppContext(null, client); 
        AppContext.CreatNewShader();
        
        ShaderRenderer.SetContext(AppContext);
    }

}