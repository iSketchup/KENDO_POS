using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Main.Models;

namespace Main.ViewModels;

public partial class CommentListViewModel : ViewModelBase
{
    public Shader? Shader { get; private set; }
    
    public ObservableCollection<Comment> comments { get; set; }

    public CommentListViewModel()
    {
        comments = new ObservableCollection<Comment>()
        {
            new Comment("Test", "Test"),
            new Comment("Test2", "Test2"),
        };
    }
    
    public CommentListViewModel(Shader shader)
    {
        shader = shader;
    }
    
    
    
    [RelayCommand]
    public void AddComment(TextBox textBox)
    {
        string? commentText = textBox.Text;
        comments.Add(new Comment($"{commentText}", $"fehlt"));
        textBox.Clear();
    }
    
}
