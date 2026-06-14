using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;

namespace Main.Models;

public interface IShaderRepository
{
    Task<List<Shader>> GetAllShaders(User user);
    Task<Shader> GetShaderById(User user, int id);
    Task UpdateShader(int uid, Shader shader);
    
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

    public async Task<Shader> GetShaderById(User user, int id)
    {
        var result = await client.GetFromJsonAsync<Shader>($"{user.Id}/shaders/{id}");
        
        return result ?? null;
    }

    public async Task UpdateShader(int uid, Shader shader)
    {
        Log.Logger.Debug("GetShaderById {id}", shader.ShaderId);
        var dto = new ShaderUpdateDto
        {
            ShaderCode = shader.ShaderCode,
            ShaderName = shader.ShaderName,
            user_id = uid,

            ShaderTextures = shader.ShaderTextures.Select((uri, index) => new TextureUpdateDto
            {
                id = index,
                Texture64 = uri
            }).ToList()
        };

        var result = await client.PutAsJsonAsync(
            $"{uid}/shaders/{shader.ShaderId}",
        dto
            );
        
        
        var body = await result.Content.ReadAsStringAsync();

        if (!result.IsSuccessStatusCode)
        {
            Log.Logger.Error("PUT failed {StatusCode}: {Body}", result.StatusCode, body);
        }

        result.EnsureSuccessStatusCode();
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

    private ObservableCollection<Uri> samplePaths = new ObservableCollection<Uri>() {new Uri("avares://Main/Assets/TEXTuffAssDino.jpg") , new Uri("avares://Main/Assets/TEXTuffAssMinion.jpg"), new Uri("avares://Main/Assets/TEXTuffAssWorm.jpg")};
            

    
    public async Task<List<Shader>> GetAllShaders(User user ) 
    {
        return  new List<Shader>()
        {
            Shader.ShaderFactory(1,sampleCode1, "Dino", new(), new(67, false), new ObservableCollection<Comment>(), samplePaths),
            Shader.ShaderFactory(2,sampleCode2, "chillax", new(), new(67, false), new ObservableCollection<Comment>(), samplePaths),
            Shader.ShaderFactory(3,sampleCode3, "evilmaxxing", new(), new(67, false), new ObservableCollection<Comment>(), samplePaths),
        };
        
    }

    public async Task<Shader> GetShaderById(User user, int id)
    {
        List<Shader> shaders = await GetAllShaders(user);
        return shaders[id];
    }

    public Task UpdateShader(int uid, Shader shader)
    {
        throw new System.NotImplementedException();
    }
}
