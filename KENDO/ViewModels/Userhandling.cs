using System;
using System.Net.Http;
using Main.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BCrypt.Net;

namespace Main.ViewModels;

public static class Userhandling
{
    // *****
    // KI-Teil
    // ChatGPT & Claude
    // Prompt: Wie kann ich HTTPS für das Login handeln?
    private static HttpClientHandler handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    // *****

    private static HttpClient client = new HttpClient(handler)
    {
        BaseAddress = new Uri("https://127.0.0.1:8000")
    };

    private static ApiService apiService = new ApiService(client);
    
    public async static Task AddUser(string name, string pswd)
    {
        string hashed = BCrypt.Net.BCrypt.HashPassword(pswd);
        User user =  new User { UserName = name, passwd = hashed };
        

        await apiService.CreateUser(user);
    }

    public async static Task<bool> ValidateLogin(string name, string pswd)
    {
        User? user = await apiService.GetUserInfo(name, pswd);
        
        if (user == null) return false;

        bool ok = BCrypt.Net.BCrypt.Verify(pswd, user.passwd);
        return ok ? true : false; // falls ok dann user falls !ok => false
    }
}