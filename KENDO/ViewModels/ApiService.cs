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

    public async Task<User?> GetUserInfo(string userName, string passwd)
    {
        User user = new User { UserName = userName, passwd = passwd };

        HttpResponseMessage response = await _client.PostAsJsonAsync("user/login", user);
        string body = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<User>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task CreateUser(User user)
    {
        // Bevor man etwas einfügt, wird auf eine Antwort der API gewartet
        HttpResponseMessage result = await _client.PostAsJsonAsync("user/", user);
        result.EnsureSuccessStatusCode();
    }
}