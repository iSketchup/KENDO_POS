using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Serilog;

namespace Main.Models;

public interface IShaderRepository
{
    Task<List<Shader>> GetAllShaders(User user);
    Task<Shader> GetShaderById(User user, int id);
    Task UpdateShader(int uid, Shader shader);
    Task<Shader> CreateNewShader(User user, string ShaderName);

    Task <List<Shader>> GetShadersByFilter(
        int userId,
        string? shaderUserName = null,
        string? shaderName = null,
        IEnumerable<string>? tags = null);

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
        var dtos = await client.GetFromJsonAsync<List<ShaderGetDto>>($"{user.Id}/shaders/");

        List<Shader> result = new ();

        foreach (var dto in dtos)
        {
            result.Add( Shader.ShaderFactory(
                dto.ShaderId,
                dto.ShaderCode,
                dto.ShaderName,
                new(dto.ShaderTags),
                dto.ShaderLikes,
                new(dto.ShaderComments),
                new(dto.ShaderTextures.Select(t => t.Texture64))
            ));
        }

        return result ?? null;
    }

    public async Task<Shader> GetShaderById(User user, int id)
    {
        var dto = await client.GetFromJsonAsync<ShaderGetDto>($"{user.Id}/shaders/{id}");
        
        return  Shader.ShaderFactory(
            dto.ShaderId,
            dto.ShaderCode,
            dto.ShaderName,
            new(dto.ShaderTags),
            dto.ShaderLikes,
            new(dto.ShaderComments),
            new(dto.ShaderTextures.Select(t => t.Texture64))
        ) ?? null;
    }

    public async Task UpdateShader(int uid, Shader shader)
    {
        Log.Logger.Debug("UpdatedById {id}", shader.ShaderId);
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

        try
        {

            result.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            Log.Logger.Fatal("User is not allowed to write this"+ e);
        }
    }

    public async Task<Shader> CreateNewShader(User user, string ShaderName)
    {
        Log.Logger.Debug("Creating Shader");
        var dtoin = new ShaderUpdateDto
        {
            ShaderName = ShaderName,
            user_id = user.Id,

            ShaderTextures = new List<TextureUpdateDto>()
        };

        var result = await client.PostAsJsonAsync(
            $"{user.Id}/shaders/new",
            dtoin
        );
        result.EnsureSuccessStatusCode();
        
        
        var dto = await result.Content.ReadFromJsonAsync<ShaderGetDto>();
        
        return  Shader.ShaderFactory(
            dto.ShaderId,
            dto.ShaderCode,
            dto.ShaderName,
            new(dto.ShaderTags),
            dto.ShaderLikes,
            new(dto.ShaderComments),
            new(dto.ShaderTextures.Select(t => t.Texture64))
        ) ?? null;
    }
    
    // AI how do i get shaders with filters
    
    public async Task<List<Shader>> GetShadersByFilter(
        int userId,
        string? shaderUserName = null,
        string? shaderName = null,
        IEnumerable<string>? tags = null)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        if (!string.IsNullOrWhiteSpace(shaderUserName))
            query["shader_user_name"] = shaderUserName;

        if (!string.IsNullOrWhiteSpace(shaderName))
            query["shader_name"] = shaderName;

        if (tags is not null)
        {
            foreach (var tag in tags)
                query.Add("tags", tag);
        }

        var url = $"{userId}/shaders/filter/?{query}";

        var dtos = await client.GetFromJsonAsync<List<ShaderGetDto>>(url);

        return dtos?.Select(MapToShader).ToList() ?? new List<Shader>();
    }

    private static Shader MapToShader(ShaderGetDto dto) =>
        Shader.ShaderFactory(
            dto.ShaderId,
            dto.ShaderCode,
            dto.ShaderName,
            new(dto.ShaderTags),
            dto.ShaderLikes,
            new(dto.ShaderComments),
            new(dto.ShaderTextures.Select(t => t.Texture64))
        );

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
        return shaders.First(s => s.ShaderId == id);
    }

    public Task UpdateShader(int uid, Shader shader)
    {
        throw new System.NotImplementedException();
    }

    public Task<Shader> CreateNewShader(User user, string ShaderName)
    {
        throw new NotImplementedException();
    }

    public Task<List<Shader>> GetShadersByFilter(
        int userId,
        string? shaderUserId = null,
        string? shaderName = null,
        IEnumerable<string>? tags = null)
    {
        throw new NotImplementedException();
    }
}
