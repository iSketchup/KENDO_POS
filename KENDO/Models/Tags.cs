using System;
using System.Collections.Generic;

namespace Main.Models;

public class Tags
{
    public List<string> Taglist { get; set; }

    public void Add(string TagName)
    {
        
    }

    public void LoadAll()
    {
        throw new NotImplementedException();
    }

    public static List<string> LoadByShader(int ShaderID)
    {
        throw new NotImplementedException();
    }
    
}