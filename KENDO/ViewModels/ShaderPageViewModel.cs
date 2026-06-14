using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Main.Models;

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


    public async Task UpdateContexts(AppContext appContext, int shader_id)
    {
        AppContext = appContext;
        
        Shader shader = await AppContext.GetShaderById(shader_id);
        
        ShaderRenderer.UpdateContexts(shader);
        Comments.SetContext(shader);
    }
    
    [RelayCommand]
    private void GoToFrontPage()
    {
        _navigation.Navigate(Page.Front);
    }

    [RelayCommand]
    private void SaveShader()
    {
        AppContext.SaveShader(ShaderRenderer.Shader);
    }
    
    
}