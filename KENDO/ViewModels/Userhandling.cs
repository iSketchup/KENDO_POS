using System;
using System.Net.Http;
using Main.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Main.ViewModels;

public static class Userhandling
{
    
    
    
    private static HttpClient client = new HttpClient()
    {
        BaseAddress = new Uri("http://127.0.0.1:8000/")
    };

    private static ApiService apiService = new ApiService(client);
    
    public static void AddUser(string name, string pswd)
    {
        new User { UserName = name, passwd = pswd };
    }

    public async static Task<User?> VaidateLogin(string name, string pswd)
    {
        List<User> users = await apiService.GetUserInfo(); // echte API

        // Das FirstOrDefault holt sich den ersten Wert von der DB, bei welcher
        // der Name und das Passwort zueinander passen.
        return users.FirstOrDefault(u => u.UserName == name && u.passwd == pswd);
    }
}