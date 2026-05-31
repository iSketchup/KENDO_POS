using System;
using System.Collections.Generic;
using System.Net.Http;
using Main.Models;

namespace Main.ViewModels;

public  class AppContext
{
    public User User { get; set; }
    
    private IShaderRepository shaderRepository;
    public Shader Shader { get; set; } 
    

    public  AppContext(User? currentUser, HttpClient? client)
    {
        if (client == null)
            FakeInit();
        else 
            AsyncInit(client);
        
        // TODO: noch zu implementieren
        User = currentUser;
        
        
    }

    private void FakeInit()
    {
        Console.WriteLine("Loading with fake repo");
        shaderRepository = new ShaderRepositoryRest(null);
        
    }

    private async void AsyncInit(HttpClient client)
    {
        Console.WriteLine("Loading with rest repo");
        shaderRepository = new ShaderRepositoryRest(client);
        
        List<Shader> shaders = await shaderRepository.GetAllShaders();

        foreach (Shader shader in await shaderRepository.GetAllShaders())
        {
            Console.WriteLine(shader.ShaderCode);
        }

    }

    
    public void CreatNewShader()
    {
        // ToDo: save old stuff + repo stuff
        
        Shader = new Shader();
        
    }

    public void switchShader(Shader shader)
    {
        Shader = shader;
    }
}