using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Main.Models;
using Serilog;

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

    public async Task FakeInit()
    {
        Log.Logger.Information("Loading fake repo");
        shaderRepository = new ShaderRepositoryFake();
        Shaders = await shaderRepository.GetAllShaders();

    }

    public async Task AsyncInit(HttpClient client)
    {
        Log.Logger.Information("Loading rest repo");
        shaderRepository = new ShaderRepositoryRest(client);
        
        Shaders = await shaderRepository.GetAllShaders();

    }
}