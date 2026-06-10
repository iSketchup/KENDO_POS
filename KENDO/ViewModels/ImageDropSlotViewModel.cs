using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Main.ViewModels;

public partial class ImageDropSlotViewModel :ViewModelBase
{
    
    [ObservableProperty]
    private bool _hasBaseSource = true;

    private Uri _imageUri = new Uri("avares://Main/Assets/AddIcon.png");

    public Uri ImageUri
    {
        get => _imageUri;
        set
        {

            if (SetProperty(ref _imageUri, value))
            {
                ImageSource = new Bitmap(Helper.OpenImageStream(value));
            } 
        }
    }

    private Bitmap _imageSource= new(AssetLoader.Open(new Uri("avares://Main/Assets/AddIcon.png")));
    public Bitmap ImageSource
    {
        get => _imageSource;
        private set
        {
            if (SetProperty(ref _imageSource, value))
            {
                HasBaseSource = false;
            }
        }
    }

    
    [RelayCommand]
    public async void AddPicture(Control control)
    {
        
        var topLevel = TopLevel.GetTopLevel(control);

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Texture",
            AllowMultiple = false, // <---
            FileTypeFilter = new[]
            {
                FilePickerFileTypes.ImageAll
            }
        });

        if (files.Count >= 1)
        {
            var file = files[0];
            
            await using var stream = await file.OpenReadAsync();
            ImageUri = file.Path;
            
        }
    }
}