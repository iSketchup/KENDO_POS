using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Main.Models;

namespace Main.ViewModels;

public partial class TagViewModel : ViewModelBase
{
    public ObservableCollection<string> shaderTags { get; set; } = new();
    
    public TagViewModel() { }
    
    [RelayCommand]
    public void DeleteTag(string tag)
    {
        shaderTags.Remove(tag);
    }
    
    [RelayCommand]
    public void AddTag(TextBox? textBox)
    {
        if (!string.IsNullOrWhiteSpace(textBox?.Text))
        {
            shaderTags.Add(textBox.Text);
            textBox.Clear();
        }

    }
    
    public void SetContext(ObservableCollection<string> s)
    {
        shaderTags = s;
    }
}