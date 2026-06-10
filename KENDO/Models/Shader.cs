using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;


namespace Main.Models;

public partial class Shader : ObservableObject
{
    public int ShaderId {get; set;}
    public string ShaderName {get; set;}
    public string ShaderCode {get; set;}
    public ObservableCollection<string> ShaderTags {get; set;} = new();
    public  Likes ShaderLikes {get; set;} = new(); 
    public ObservableCollection<Comment> ShaderComments { get; set; } = new();

    public List<string> ShadersTextures
    {
        set
        {
            foreach (string path in value)
            {
                Textures.Add(new Uri(path));
            }
        }
    }  
    
    [JsonIgnore] [ObservableProperty] public ObservableCollection<Uri> _textures = new();
    
    
    

    public static Shader ShaderFactory(int Id, string Code, string Name, ObservableCollection<string> Tags, Likes likes, ObservableCollection<Comment> Comments,
        List<string> Textures)
    {
        return new Shader
        {
            ShaderId = Id,
            ShaderCode = Code,
            ShaderName = Name,
            ShaderTags = Tags,
            ShaderLikes = likes,
            ShaderComments = Comments,
            ShadersTextures = Textures,
        };
    }


}