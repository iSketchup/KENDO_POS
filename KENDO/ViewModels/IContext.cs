using System.Threading.Tasks;

namespace Main.ViewModels;

public interface IContext
{
    
    public abstract Task UpdateContexts(AppContext appContext);
}