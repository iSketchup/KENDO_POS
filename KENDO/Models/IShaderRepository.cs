using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Main.Models;

public interface IShaderRepository
{
    Task<List<Shader>> GetAllShaders();
    Task<Shader> GetShaderById(int id);
    void updateShader(int uid, int sid);
    
}

public class ShaderRepositoryRest : IShaderRepository
{
    private HttpClient client;
    
    // Dependency Injection
    public ShaderRepositoryRest(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<Shader>> GetAllShaders()
    {
        var result = await client.GetFromJsonAsync<List<Shader>>("shaders");
        
        return result ?? new List<Shader>();
        // Returns result if result != null -> otherwise the right part so a new List
    }

    public Task<Shader> GetShaderById(int id)
    {
        throw new System.NotImplementedException();
    }

    public void updateShader(int uid, int sid)
    {
        throw new System.NotImplementedException();
    }
}