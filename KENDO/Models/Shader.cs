using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Main.Models;

public partial class Shader : ObservableObject
{
    public int ShaderId { get;  set; }

    [ObservableProperty] private string _shaderCode;

    public int ShaderLikes { get;  set; }
    public ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();
    public ObservableCollection<string> ShaderTags { get; set; } = new ObservableCollection<string>();

    [ObservableProperty] private List<Uri> _textures = new List<Uri>();

    public Shader(string shaderCode, int shaderId, List<string> Paths)
    {
        ShaderId = shaderId;
        ShaderCode = shaderCode;

        foreach (string path in Paths)
        {
            Textures.Add(new Uri(path));
        }
    }

    public void LoadShader()
    {
        Comments = Comment.LoadComments(ShaderId);
        ShaderTags = Tags.LoadByShader(ShaderId);
        ShaderLikes = Likes.GetLikesAmount(ShaderId);
    }
}