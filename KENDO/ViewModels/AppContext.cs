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
    

    public  AppContext(User? currentUser)
    {
        User = currentUser;
        
    }

    public async Task FakeInit()
    {
        Log.Logger.Information("Loading fake repo");
        shaderRepository = new ShaderRepositoryFake();

    }

    public async Task AsyncInit(HttpClient client)
    {
        Log.Logger.Information("Loading rest repo");
        shaderRepository = new ShaderRepositoryRest(client);
        

    }

    public async Task<List<Shader>> GetAllShaders()
    {
        return await shaderRepository.GetAllShaders();   
    }

    public async Task<Shader?> GetShaderById(int id)
    {
        return await shaderRepository.GetShaderById(id);  
    }
    
}