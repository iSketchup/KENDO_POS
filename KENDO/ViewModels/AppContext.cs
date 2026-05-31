using Main.Models;

namespace Main.ViewModels;

public class AppContext
{
    public User User { get; set; }
    public Shader Shader { get; set; } 

    public AppContext(User? currentUser)
    {
        // TODO: noch zu implementieren
        User = currentUser;
    }

    public void CreatNewShader()
    {
        // ToDo: save old stuff
        
        Shader = new Shader();
        
    }

    public void switchShader(Shader shader)
    {
        Shader = shader;
    }
}