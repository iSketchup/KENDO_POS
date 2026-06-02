using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

    public static ObservableCollection<string> LoadByShader(int ShaderID)
    {
        throw new NotImplementedException();
    }
    
}