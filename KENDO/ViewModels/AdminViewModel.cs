using System.Threading.Tasks;

namespace Main.ViewModels;

public class AdminViewModel: ViewModelBase
{
    private AppContext appContext;
    
    private readonly NavigationService _navigation;
    
    public AdminViewModel(NavigationService navigation)
    {
        _navigation = navigation;
    }
    
    public async Task UpdateContexts(AppContext appContext)
    {
        this.appContext = appContext;
    }
}