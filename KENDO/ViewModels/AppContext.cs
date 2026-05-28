using Main.Models;

namespace Main.ViewModels;

public class AppContext
{
    public User CurrentUser { get; set; }
    public Shader CurrentShader { get; set; } 

    public AppContext(User? currentUser)
    {
        // TODO: noch zu implementieren
        CurrentUser = currentUser;
    }

    public void switchShader(Shader shader)
    {
        CurrentShader = shader;
    }
}