using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using SimpleJSON;


namespace Main.Models;

public partial class Shader : ObservableObject
{
    public int ShaderId {get; set;}
    public string ShaderName {get; set;}
    public string ShaderCode {get; set;}
    public ObservableCollection<string> ShaderTags {get; set;} = new();
    
    [ObservableProperty] private  Likes _shaderLikes = new(); 
    public ObservableCollection<Comment> ShaderComments { get; set; } = new();
    
    [ObservableProperty] public ObservableCollection<Uri> _shaderTextures = new();
    


    public static Shader ShaderFactory(int Id, string Code, string Name, ObservableCollection<string> Tags, Likes likes, ObservableCollection<Comment> Comments,
        ObservableCollection<Uri> Textures)
    {
        return new Shader
        {
            ShaderId = Id,
            ShaderCode = Code,
            ShaderName = Name,
            ShaderTags = Tags,
            ShaderLikes = likes,
            ShaderComments = Comments,
            ShaderTextures = Textures,
        };
    }


}
public class ShaderGetDto
{
    public int ShaderId { get; set; }
    public string ShaderName { get; set; } = "";
    public string ShaderCode { get; set; } = "";
    public List<string> ShaderTags { get; set; } = new();
    public Likes ShaderLikes { get; set; } = new();
    public List<Comment> ShaderComments { get; set; } = new();
    public List<TextureUpdateDto> ShaderTextures { get; set; } = new();
}
public class ShaderUpdateDto
{
    [JsonPropertyName("ShaderCode")]
    public string ShaderCode { get; set; } = "";

    [JsonPropertyName("ShaderName")]
    public string ShaderName { get; set; } = "";

    [JsonPropertyName("user_id")]
    public int user_id { get; set; }

    [JsonPropertyName("ShaderTextures")]
    public List<TextureUpdateDto> ShaderTextures { get; set; } = new();
}

public class TextureUpdateDto
{
    [JsonPropertyName("id")]
    public int id { get; set; }

    [JsonPropertyName("Texture64")]
    [JsonConverter(typeof(ImageUriBase64Converter))]
    public Uri Texture64 { get; set; } = default!;
}