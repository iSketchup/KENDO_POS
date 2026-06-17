using System;
using System.IO;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;

namespace Main.ViewModels;

public partial class LikesViewModel : ViewModelBase
{ 
    public Shader? Shader { get; private set; }

    [ObservableProperty] private Bitmap _likeTexture;
    
    public LikesViewModel() { }
    
    public void SetContext(Shader shader)
    {
        Shader = shader;
        if (Shader.ShaderLikes.liked_by_u)
        {
            LikeTexture = new Bitmap(Helper.OpenImageStream(Likes.LikedTexture));
        }
        else
        {
            LikeTexture = new Bitmap(Helper.OpenImageStream(Likes.NotLikedTexture));
        }
    }
    
    [RelayCommand]
    public void UserLiked()
    {
        if (Shader.ShaderLikes.liked_by_u)
        {
            Shader.ShaderLikes.liked_by_u = false;
            LikeTexture = new Bitmap(Helper.OpenImageStream(Likes.NotLikedTexture));
            Shader.ShaderLikes.Amount -= 1;
        }
        else
        {
            Shader.ShaderLikes.liked_by_u = true;
            LikeTexture = new Bitmap(Helper.OpenImageStream(Likes.LikedTexture));
            Shader.ShaderLikes.Amount += 1;
        }
    }
    
    
}