using System;
using System.Collections.Generic;

namespace Main.Models;

public class Comment
{
    public int Id { get; private set; }
    public string Text { get; private set; }
    public string Author { get; private set; }
    
    
    public Comment(string text, string author)
    {
        Text = text;
        Author = author;
    }
    
    public Comment(string text, string author, int id) : base()
    {
        Id = id;
    }

    public override string ToString()
    {
        return $"{Author}: {Text}";
    }

    public static List<Comment> LoadComments(int ShaderID)
    {
        throw new NotImplementedException();
    }
}