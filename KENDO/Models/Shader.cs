using System.Collections.Generic;

namespace Main.Models;

public class Shader
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int Likes { get; set; }
    public List<Comment> Comments { get; set; }
    public List<Tags> Tags { get; set; }
}