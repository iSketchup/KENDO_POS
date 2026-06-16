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
    
    private ILikeRepository likeRepository;
    
    private ICommentRepository commentRepository;
    
    private ITagRepository tagRepository;
    

    public  AppContext(User? currentUser)
    {
        // Aktueller oder leerer User
        User = currentUser ?? new User();
    }

    public async Task FakeInit()
    {
        Log.Logger.Information("Loading fake repo");
        shaderRepository = new ShaderRepositoryFake();
        likeRepository = new LikeRepositoryFake();
        commentRepository = new CommentRepositoryFake();
        tagRepository = new TagRepositoryFake();

    }

    public async Task AsyncInit(HttpClient client)
    {
        Log.Logger.Information("Loading rest repo");
        shaderRepository = new ShaderRepositoryRest(client);
        likeRepository = new LikeRepositoryRest(client);
        commentRepository = new CommentRepositoryRest(client);
        tagRepository = new TagRepositoryRest(client);
        

    }

    public async Task<List<Shader>> GetAllShaders()
    {
        return await shaderRepository.GetAllShaders(User);   
    }

    public async Task<Shader?> GetShaderById(int shaderid)
    {
        return await shaderRepository.GetShaderById(User, shaderid);  
    }

    public async Task SaveShader(Shader shader)
    {
        await shaderRepository.UpdateShader(User.Id, shader);
    }

    /// <summary>
    /// creates a new shader from the reposetory
    /// </summary>
    /// <param name="shaderName"></param>
    /// <returns>Returns the shader id of the newly built shader</returns>
    public async Task<int> CreateNewShader(string shaderName)
    {
        return (await shaderRepository.CreateNewShader(User, shaderName)).ShaderId;
    }

    public async Task CreateComment(int shader_id, string CommentText)
    {
        await commentRepository.AddComment(User.Id, shader_id, CommentText);
    }
    
}