using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Main.Models;

public interface IShaderRepository
{
    Task<List<Shader>> GetAllShaders(User user);
    Task<Shader> GetShaderById(User user, int id);
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

    public async Task<List<Shader>> GetAllShaders(User user)
    {
        var result = await client.GetFromJsonAsync<List<Shader>>($"{user.Id}/shaders");
        
        return result ?? new List<Shader>();
        // Returns result if result != null -> otherwise the right part so a new List
    }

    public async Task<Shader> GetShaderById(User user,  int id)
    {
        var result = await client.GetFromJsonAsync<Shader>($"{user.Id}/shaders/{id}");
        
        return result ?? null;
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
                                    outputColor = vec4(0.6,0.7,0,1);
                                 }
                                 """;

    private string sampleCode3 = """
                                 #version 330
                                 out vec4 outputColor;
                                 in vec2 TexCoord;
                                 uniform sampler2D texture0;
                                 uniform sampler2D texture1;
                                 uniform float uTime;

                                 void main()
                                 {
                                     vec4 tex0 = texture(texture0, TexCoord);
                                     outputColor = vec4(1-tex0.rbg, tex0.a);
                                 }
                                 """;

    private List<string> samplePaths = new List<string> {"avares://Main/Assets/TEXTuffAssDino.jpg" , "avares://Main/Assets/TEXTuffAssDino.jpg", "avares://Main/Assets/TEXTuffAssDino.jpg"};
            

    
    public async Task<List<Shader>> GetAllShaders(User user ) 
    {
        return  new List<Shader>()
        {
            new(sampleCode1, 1, samplePaths), new(sampleCode2, 2, samplePaths), new(sampleCode3, 3, samplePaths)
        };
        
    }

    public async Task<Shader> GetShaderById(User user, int id)
    {
        List<Shader> shaders = await GetAllShaders(user);
        return shaders[id];
    }

    public void updateShader(int uid, int sid)
    {
        throw new System.NotImplementedException();
    }
}
