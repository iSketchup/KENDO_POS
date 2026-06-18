using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenTK.Graphics.OpenGL;
using Serilog;

namespace Main.ViewModels;

public partial class UrlPickerViewModel :ViewModelBase
{

    [ObservableProperty] private string _url = "";
    [ObservableProperty] private string _port = "";
    
    private readonly NavigationService _navigation;

    public UrlPickerViewModel(NavigationService navigation)
    {
        _navigation = navigation;
    }
    

    public static bool MatchesRegex(string pattern, string input)
    {
        return Regex.IsMatch(input, pattern);
    }

    [RelayCommand]
    public async void Submit()
    {
        bool re = false;
        if (!MatchesRegex(@"^(((?!25?[6-9])[12]\d|[1-9])?\d\.?\b){4}$",Url))
        {
            Url = "Please use a correctly formated Ip";
            re = true;
        }
        if (!MatchesRegex(@"^\d{1,4}$",Port))
        {
            Port = "Please use a correctly formated Port";
            re = true;
        }
        
        
        var uriText = $"https://{Url}:{Port}/";

        if (!Uri.TryCreate(uriText, UriKind.Absolute, out var uri))
        {
            Url = "Invalid server address";
            return;
        }


        MainWindowViewModel.baseadress = new Uri(uriText);

        
        if (!await CheckConnection())
            re = true;
        
        

        if (re)
            return;

        _navigation.NavigateRequested(Page.Login);
    }

    private async Task<bool> CheckConnection()
    {
        
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        using var client = new HttpClient(handler)
        {
            BaseAddress = MainWindowViewModel.baseadress,
        };

        try
        {
            var response = await client.GetAsync("");

            if (!response.IsSuccessStatusCode)
            {
                Url = $"Server error: {(int)response.StatusCode}";
                return false;
            }
        }
        catch (Exception e)
        {
            Log.Logger.Fatal("Exception while checking server connection" + e);
            Url = "Could not connect to server";
            return false;
        }

        return true;

    }
    
}