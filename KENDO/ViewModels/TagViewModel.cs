using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Main.Models;

namespace Main.ViewModels;

public partial class TagViewModel : ViewModelBase
{
    AppContext appContext { get; set; }
    
    public Shader shader { get; private set; }
    
    
    public TagViewModel() { }
    
    [RelayCommand]
    public void DeleteTag(string tag)
    {
        shader.ShaderTags.Remove(tag);
    }
    
    [RelayCommand]
    public async Task AddTag(TextBox? textBox)
    {
        if (!string.IsNullOrWhiteSpace(textBox?.Text))
        {
            shader.ShaderTags.Add(textBox.Text);
            //await appContext.CreateAndAssignTag(textBox.Text, shader.ShaderId, appContext.User.Id);
            await appContext.CreatTag(textBox.Text);
            textBox.Clear();
        }
    }
    
    public void SetContext(Shader s, AppContext app)
    {
        shader = s;
        appContext = app;
    }
}


