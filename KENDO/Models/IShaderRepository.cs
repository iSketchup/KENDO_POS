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

public class ShaderRepositoryFake : IShaderRepository
{
    private string sampleCode1 = """
                                   #version 330

                                   out vec4 outputColor;

                                   in vec2 TexCoord;

                                   uniform sampler2D texture0;
                                   uniform sampler2D texture1;
                                   uniform float uTime;

                                   void main()
                                   {
                                        vec4 tex0 = texture(texture0, TexCoord * sin(uTime)+1.2);
                                        vec4 tex1 = texture(texture1, TexCoord) * cos(uTime)*0.5;
                                        outputColor = mix(tex0,tex1,0.3);
                                   }
                                   """; 

    private string sampleCode2 = """
                                 #version 330

                                 out vec4 outputColor;

                                 in vec2 TexCoord;

                                 uniform sampler2D texture0;
                                 uniform sampler2D texture1;
                                 uniform float uTime;

                                 void main()
                                 {
                                    outputColor = vex4 (0.5);
                                 }
                                 """; 

    
    public async Task<List<Shader>> GetAllShaders()
    {
        return  new List<Shader>() {new Shader(sampleCode1, 1), new Shader(sampleCode2, 2)};
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
