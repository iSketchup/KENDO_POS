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
    private static HttpClient client = new HttpClient()
    {
        BaseAddress = new Uri("http://127.0.0.1:8000/")
    };

    private static ApiService apiService = new ApiService(client);
    
    public async static Task AddUser(string name, string pswd)
    {
        string hashed = BCrypt.Net.BCrypt.HashPassword(pswd);
        User user =  new User { UserName = name, passwd = hashed };
        

        await apiService.CreateUser(user);
    }

    public async static Task<User?> ValidateLogin(string name, string pswd)
    {
        User? user = await apiService.GetUserInfo(name, pswd);
        
        // Das FirstOrDefault holt sich den ersten Wert von der DB, bei welcher
        // der Name und das Passwort zueinander passen.
        //var user = users.FirstOrDefault(u => u.UserName == name);
        if (user == null) return null;

        //bool ok = BCrypt.Net.BCrypt.Verify(pswd, user.passwd);
        //return ok ? user : null; // falls ok dann user falls !ok => null
        return user;
    }
}