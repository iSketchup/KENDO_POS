using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Main.Models;
using System.Text;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Threading.Tasks;

namespace Main.ViewModels;

public class ApiService
{
    private HttpClient _client;

    public ApiService(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<List<User>> GetUserInfo()
    {
        List<User>? response = await _client.GetFromJsonAsync<List<User>>("user/");

        // Neue Liste wird erzeugt, falls der User nicht geholt werden kann.
        return response ?? new List<User>();
    }

    public async Task CreateUser(User user)
    {
        // Bevor man etwas einfügt, wird auf eine Antwort der API gewartet
        HttpResponseMessage result = await _client.PostAsJsonAsync("user/", user);
        result.EnsureSuccessStatusCode();
    }
}