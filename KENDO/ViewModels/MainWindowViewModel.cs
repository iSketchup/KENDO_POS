using System;
using System.Net.Http;
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