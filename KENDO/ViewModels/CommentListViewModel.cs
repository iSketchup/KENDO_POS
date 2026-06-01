using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Main.Models;

namespace Main.ViewModels;

public partial class CommentListViewModel : ViewModelBase
{
    public Shader? Shader { get; private set; }
    
    
    public CommentListViewModel() { }
    
    [RelayCommand]
    public void AddComment(TextBox textBox)
    {
        if (textBox.Text != "")
        {
            Shader.Comments.Add(new Comment($"{textBox.Text}", $"fehtl"));
            textBox.Clear();
        }
    }
    
    [RelayCommand]
    public void DeleteTag(string tag)
    {
        Shader.ShaderTags.Remove(tag);
    }
    
    [RelayCommand]
    public void AddTag(TextBox textBox)
    {
        if (textBox.Text != "")
        {
            Shader.ShaderTags.Add(textBox.Text);
            textBox.Clear();
        }

    }
    
    public void SetContext(AppContext a, int Shader_id)
    {
        this.Shader = a.Shaders[Shader_id];
    }
}
