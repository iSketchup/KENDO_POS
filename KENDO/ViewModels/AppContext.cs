using System;
using System.Collections.Generic;
using System.Net.Http;
using Main.Models;

namespace Main.ViewModels;

public  class AppContext
{
    public User User { get; set; }
    
    private IShaderRepository shaderRepository;
    public List<Shader> Shaders { get; set; } 
    

    public  AppContext(User? currentUser, HttpClient? client)
    {
        if (client == null)
            FakeInit();
        else 
            AsyncInit(client);
        
        // TODO: noch zu implementieren
        User = currentUser;
        
        
    }

    private async void FakeInit()
    {
        Console.WriteLine("Loading with fake repo");
        shaderRepository = new ShaderRepositoryFake();
        Shaders = await shaderRepository.GetAllShaders();

    }

    private async void AsyncInit(HttpClient client)
    {
        Console.WriteLine("Loading with rest repo");
        shaderRepository = new ShaderRepositoryRest(client);
        
        Shaders = await shaderRepository.GetAllShaders();

    }
}