using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Main.Models;

public class Likes
{
    
    public int amount {get; set;}
    public bool liked_by_u {get; set;}

    public static Uri LikedTexture = new ("avares://Main/Assets/heart-solid.png");

    public static Uri NotLikedTexture = new ("avares://Main/Assets/heart-regular.png");
    
    

    public Likes()
    {
    }
    
    public Likes(int amount, bool liked_by_u)
    {
        this.amount = amount;
        this.liked_by_u = liked_by_u;
    }
    
    
    public static void AddLike(int ShaderId, string UserName)
    {
        throw new NotImplementedException();
        
    }

    public static void RemoveLike(int ShaderId, string UserName)
    {
        throw new NotImplementedException();
        
    }

    public static int GetLikesAmount(int ShaderId)
    {
        throw new NotImplementedException();
    }
}