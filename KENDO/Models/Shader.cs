using System.Collections.Generic;

namespace Main.Models;

public class Shader
{
    public int Id { get; private set; }
    public string Code { get; set; }
    public int ShaderLikes { get;private set; }
    public List<Comment> Comments { get; set; }
    public List<string> ShaderTags { get; set; }

    public void LoadShader()
    {
        Comments = Comment.LoadComments(Id);
        ShaderTags = Tags.LoadByShader(Id);
        ShaderLikes = Likes.GetLikesAmount(Id);
    }
}