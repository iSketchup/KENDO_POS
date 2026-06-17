using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Main.Views;
using Serilog;

namespace Main.ViewModels;

public partial class ShaderRendererViewModel : ViewModelBase
{
    [ObservableProperty] 
    private TextDocument _document;
    
    [ObservableProperty] private Shader _shader = new ();

    [ObservableProperty] private ObservableCollection<ImageDropSlotViewModel> _imageDropSlots = new() ;
    
    public LikesViewModel Like { get; } = new LikesViewModel(); 
    
    public TagViewModel Tag { get; } = new TagViewModel(); 
    
    
    private readonly NavigationService _navigation;
    
    public ShaderRendererViewModel(NavigationService navigation)
    {
        Document = new TextDocument();
        Document.TextChanged += (_, _) =>
        {
            Shader.ShaderCode = Document.Text;
        };

        _navigation = navigation;
        
        AddImageDropSlot();
    }
    
    
    private void AddImageDropSlot()
    {
        var slot = new ImageDropSlotViewModel();

        slot.PropertyChanged += OnImageDropSlotPropertyChanged;
        slot.Number = ImageDropSlots.Count;

        ImageDropSlots.Add(slot);
    }    
    private void AddImageDropSlot(Uri uri)
    {
        var slot = new ImageDropSlotViewModel();
        slot.ImageUri= uri;
        slot.Number = ImageDropSlots.Count;
        slot.PropertyChanged += OnImageDropSlotPropertyChanged;

        ImageDropSlots.Add(slot);
    }

    private void OnImageDropSlotPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        
            if (e.PropertyName != nameof(ImageDropSlotViewModel.ImageUri) &&
                e.PropertyName != nameof(ImageDropSlotViewModel.HasBaseSource))
                return;

            UpdateShaderTextures();
            EnsureEmptySlotAtEnd();
        
    }

    private void UpdateShaderTextures()
    {
        Shader.ShaderTextures.Clear();
        foreach (ImageDropSlotViewModel slot in ImageDropSlots)
        {
            if (slot.HasBaseSource)
                continue;
            
            Shader.ShaderTextures.Add(slot.ImageUri);
        }
    }
    
    private void EnsureEmptySlotAtEnd()
    {
        if (ImageDropSlots.Count == 0)
        {
            AddImageDropSlot();
            return;
        }

        var lastSlot = ImageDropSlots[ImageDropSlots.Count - 1];

        if (!lastSlot.HasBaseSource)
        {
            AddImageDropSlot();
        }
    }
    
    public void UpdateContexts(Shader shader)
    {
        Shader = shader;

        ImageDropSlots.Clear();
        foreach (Uri path in Shader.ShaderTextures)
        {
            
            
            AddImageDropSlot(path);
        }
        AddImageDropSlot();
        
        Log.Logger.Debug("Updating shader context for " + Shader.ShaderId);

        
        Document.Text = Shader.ShaderCode;
        Like.SetContext(Shader);
    }
    
    [RelayCommand]
    public void GoToThisShader()
    {
        int id = Shader.ShaderId;

        _navigation.NavigateRequestedId(id);
        
        Log.Logger.Information("Opened shader: "+id );
    }
}