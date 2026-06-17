using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Main.Models;

namespace Main.ViewModels;

public partial class CommentListViewModel : ViewModelBase
{
    private AppContext appContext { get; set; }
    public Shader? Shader { get; private set; }
    public LikesViewModel Like { get; } = new LikesViewModel(); 
    
    public TagViewModel Tag { get; } = new TagViewModel();
    
    public CommentListViewModel() { }
    
    [RelayCommand]
    public void AddComment(TextBox textBox)
    {
        if (!string.IsNullOrWhiteSpace(textBox?.Text))
        {
            Shader.ShaderComments.Add(new Comment($"{textBox.Text}", appContext.User.UserName));
            appContext.CreateComment(Shader.ShaderId, textBox.Text);
            textBox.Clear();
        }

        

    }
    

    
    public void SetContext(Shader s, AppContext app)
    {
        appContext = app;
        this.Shader = s;
        Like.SetContext(Shader, appContext);  
        Tag.SetContext(Shader, appContext);
    }
}
