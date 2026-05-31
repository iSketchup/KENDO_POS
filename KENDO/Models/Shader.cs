using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Main.Models;

public partial class Shader : ObservableObject
{
    public int ShaderId { get;  set; }

    [ObservableProperty]
    private string _shaderCode  = """
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

    public int ShaderLikes { get;  set; }
    public List<Comment> Comments { get; set; }
    public List<string> ShaderTags { get; set; }

    public void LoadShader()
    {
        Comments = Comment.LoadComments(ShaderId);
        ShaderTags = Tags.LoadByShader(ShaderId);
        ShaderLikes = Likes.GetLikesAmount(ShaderId);
    }
}