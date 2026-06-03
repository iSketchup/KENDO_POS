using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Main.Models;

namespace Main.ViewModels;

public partial class FrontPageViewModel: ViewModelBase, IContext
{
   public AppContext AppContext { get; set; }

   public FilterSelectionViewModel FilterSelection { get; set; }
   
   [ObservableProperty] private List<ShaderRendererViewModel> _shaderPages;
    
    public void UpdateContexts(AppContext appContext)
    {
        AppContext = appContext;
        
        for (int i = 0; i > AppContext.Shaders.Count; i++)
        {
            ShaderRendererViewModel spvm = new ShaderRendererViewModel();
            spvm.UpdateContexts(AppContext,i);
            ShaderPages.Add(spvm);
            
        }
    }
}