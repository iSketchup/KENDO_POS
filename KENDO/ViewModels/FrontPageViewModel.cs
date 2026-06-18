using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Serilog;

namespace Main.ViewModels;

public partial class FrontPageViewModel: ViewModelBase, IContext
{
   public AppContext AppContext { get; set; }

   
   private readonly NavigationService _navigation;
   
   public FilterSelectionViewModel FilterSelection { get; set; } = new();
   
   
   
   [ObservableProperty] 
   private ObservableCollection<ShaderRendererViewModel> _shaderPages = new ();
    
   public FrontPageViewModel(NavigationService navigation)
   {
       _navigation = navigation;
   }
   
   public Task UpdateContexts(AppContext appContext) =>
       UpdateContexts(appContext, null, null, null);
   
    public async Task UpdateContexts(AppContext appContext, string? shaderUsername = null, string? shaderName = null, IEnumerable<string>? tags = null)
    {
        
        ShaderPages.Clear();
        
        AppContext = appContext;
        
        List<Shader> shaders =  await AppContext.GetShadersByFilter(shaderUsername, shaderName, tags);
        
        
        for (int i = 0; i < shaders.Count; i++)
        {
            ShaderRendererViewModel spvm = new ShaderRendererViewModel(_navigation);
            spvm.UpdateContexts(shaders[i], appContext);
            ShaderPages.Add(spvm);
            
        }
        
        FilterSelection.SetContext(appContext, shaders, this);
        
    }

}