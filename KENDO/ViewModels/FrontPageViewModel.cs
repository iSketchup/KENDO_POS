using System.Collections.Generic;
using System.Collections.ObjectModel;
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
   
    public void UpdateContexts(AppContext appContext)
    {
        
        ShaderPages.Clear();
        
        AppContext = appContext;
        
        for (int i = 0; i < AppContext.Shaders.Count; i++)
        {
            ShaderRendererViewModel spvm = new ShaderRendererViewModel(_navigation);
            spvm.UpdateContexts(AppContext,i);
            ShaderPages.Add(spvm);
            
        }
    }

}