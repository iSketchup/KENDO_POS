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
    public async void DeleteTag(string tag)
    {
        await appContext.DeleteTagByName(tag, appContext.User.Id, shader.ShaderId);
        shader.ShaderTags.Remove(tag);
    }
    
    [RelayCommand]
    public async Task AddTag(TextBox? textBox)
    {
        if (!string.IsNullOrWhiteSpace(textBox?.Text))
        {
            await appContext.CreateAndAssignTag(textBox.Text, shader.ShaderId, appContext.User.Id);
            shader.ShaderTags.Add(textBox.Text);
            //await appContext.CreatTag(textBox.Text);
            textBox.Clear();
        }
    }
    
    public void SetContext(Shader s, AppContext app)
    {
        shader = s;
        appContext = app;
    }
}


