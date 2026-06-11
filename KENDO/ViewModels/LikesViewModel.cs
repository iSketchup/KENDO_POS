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
    [ObservableProperty] 
    private Likes _like;

    [ObservableProperty] private Bitmap _likeTexture;
    
    public LikesViewModel() { }
    
    public void SetContext(Likes like)
    {
        Like = like;
        if (Like.liked_by_u)
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
        if (Like.liked_by_u)
        {
            Like.liked_by_u = false;
            LikeTexture = new Bitmap(Helper.OpenImageStream(Likes.NotLikedTexture));
            Like.amount -= 1;
        }
        else
        {
            Like.liked_by_u = true;
            LikeTexture = new Bitmap(Helper.OpenImageStream(Likes.LikedTexture));
            Like.amount += 1;
        }
    }
    
    
}