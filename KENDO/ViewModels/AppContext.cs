using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Main.Models;

namespace Main.ViewModels;

public  class AppContext
{
    public User User { get; set; }
    
    private IShaderRepository shaderRepository;
    public List<Shader> Shaders { get; set; } 
    

    public  AppContext(User? currentUser)
    {
        User = currentUser;
        
        
    }

    public async void FakeInit()
    {
        Console.WriteLine("Loading with fake repo");
        shaderRepository = new ShaderRepositoryFake();
        Shaders = await shaderRepository.GetAllShaders();

    }

    public async Task AsyncInit(HttpClient client)
    {
        Console.WriteLine("Loading with rest repo");
        shaderRepository = new ShaderRepositoryRest(client);
        
        Shaders = await shaderRepository.GetAllShaders();

    }
}