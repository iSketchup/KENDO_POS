using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Main.Models;
using System.Text.Json;
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

    public async Task<User?> GetLogin(string userName, string passwd)
    {
        User user = new User() { UserName = userName, passwd = passwd };

        HttpResponseMessage response = await _client.PostAsJsonAsync("user/login", user);
        string body = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<User>(
            body,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<User?> GetUserInfo(string username, string passwd)
    {
        User user = new User { UserName = username };

        HttpResponseMessage response = await _client.PostAsJsonAsync($"user/{username}", user);
        string body = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<User>(
            body,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }


    // Diese Methoden ermöglichen die Verbindung auf den Endpunkt
    // mit den jeweiligen Methoden.
    public async Task CreateUser(User user)
    {
        // Bevor man etwas einfügt, wird auf eine Antwort der API gewartet
        HttpResponseMessage result = await _client.PostAsJsonAsync("user/", user);
        result.EnsureSuccessStatusCode();
    }


    public async Task ChangeUser(string username, User user)
    {
        HttpResponseMessage result = await _client.PutAsJsonAsync($"user/?username={username}", user);
        result.EnsureSuccessStatusCode();
    }



    public async Task DeleteUser(string username)
    {
        HttpResponseMessage result = await _client.DeleteAsync($"user/?username={username}");
        result.EnsureSuccessStatusCode();
    }
}