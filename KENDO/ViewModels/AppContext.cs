using Main.Models;

namespace Main.ViewModels;

public class AppContext
{
    private AppContext instance;
    public User CurrentUser { get; set; }
    public Shader CurrentShader { get; set; }

    public AppContext GetInstance()
    {
        // TODO: noch zu implementieren
        return instance;
    }
}