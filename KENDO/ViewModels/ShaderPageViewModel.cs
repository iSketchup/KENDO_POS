using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace Main.ViewModels;

public partial class ShaderPageViewModel : ViewModelBase
{
    
    
    private readonly NavigationService _navigation;

    public ShaderRendererViewModel ShaderRenderer { get; }
    public CommentListViewModel Comments { get; } = new CommentListViewModel();

    public AppContext AppContext { get; private set; }
    
    public ShaderPageViewModel(NavigationService navigation)
    {
        ShaderRenderer = new ShaderRendererViewModel(navigation);
        _navigation = navigation;
    }


    public void UpdateContexts(AppContext appContext, int shader_id)
    {
        AppContext = appContext;
        
        ShaderRenderer.UpdateContexts(AppContext, shader_id);
        Comments.SetContext(AppContext, shader_id);
    }
    
    [RelayCommand]
    private void GoToFrontPage()
    {
        _navigation.Navigate(Page.Front);
    }
    
    
}