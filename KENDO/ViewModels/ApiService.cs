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
using Serilog;

namespace Main.ViewModels;

public class ApiService
{
    private string apiKey;
    private HttpClient _client;

    public ApiService(HttpClient client)
    {
        _client = client;

        apiKey = Environment.GetEnvironmentVariable("KENDO_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            Log.Logger.Fatal("API_KEY environment variable not Found");
            throw new KeyNotFoundException();
        }


        _client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
    }

    public async Task<User?> GetLogin(string userName, string passwd)
    {
        User user = new User() { UserName = userName, Passwd = passwd };

        HttpResponseMessage response = await _client.PostAsJsonAsync("user/login", user);
        string body = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<User>(
            body);
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

    
    public async Task CreateUser(User user)
    {
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




    // *** Admin Teil ***
    public async Task CreateAdmin(User user)
    {
        // Bevor man etwas einfügt, wird auf eine Antwort der API gewartet
        HttpResponseMessage result = await _client.PostAsJsonAsync("admin/", user);
        result.EnsureSuccessStatusCode();
    }
    

    public async Task<List<User>> GetAllUsers()
    {
        var response = await _client.GetAsync("/admin"); 
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<User>>() ?? new List<User>();
        }
        return new List<User>();
    }

    // ******************
}