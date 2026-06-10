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
    /*private static HttpClientHandler handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
*/
    // *****
    private static HttpClient client = new HttpClient(
        // *****
        // KI-Teil: Claude
        // Prompt: Wie verhindere ich in C# bei HttpClient zuverlässig Socket-Timeouts bzw.
        // wie konfiguriere ich Timeouts und Connection-Reuse korrekt mit SocketsHttpHandler,
        // ohne dass die Anwendung bei Netzwerkproblemen abstürzt?
            new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(15),
                SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                {
                    RemoteCertificateValidationCallback = (msg, cert, chain, errors) => true
                }
            }
        )
    // *****
    {
        BaseAddress = new Uri("https://127.0.0.1:8000")
    };

    private static ApiService apiService = new ApiService(client);
    

    public async static Task<bool> AddUser(string name, string pswd)
    {
        User? gettingUser = await apiService.GetUserInfo(name, pswd);

        if (gettingUser.UserName == name)
        {
            return false;
        }

        string hashed = BCrypt.Net.BCrypt.HashPassword(pswd);
        User user =  new User { UserName = name, passwd = hashed };
        

        await apiService.CreateUser(user);


        return true;
    }



    public async static Task ChangeUser(string username, string name, string newpswd)
    {
        // Das veränderte Passwort sollte auch gehasht werden
        User? gettingUser = await apiService.GetLogin(username, newpswd);

        if (gettingUser != null && gettingUser.UserName != username)
        {
            throw new Exception("The username must not be the" +
                " same as the previous username");
        }

        string hashed = BCrypt.Net.BCrypt.HashPassword(newpswd);
        User updatedUser = new User { UserName = name, passwd = hashed };


        await apiService.ChangeUser(username, updatedUser);
    }



    public async static Task DeleteUser(string username)
    {
        // Einfach nur nach den Namen suchen und dann diesen User löschen.
        // Sehr simple
        await apiService.DeleteUser(username);
    }



    public async static Task<bool> ValidateLogin(string name, string pswd)
    {
        User? user = await apiService.GetLogin(name, pswd);
        
        // Ist ein Passwort vorhanden?
        if (user == null || string.IsNullOrWhiteSpace(user.passwd)) return false;
        else
        {
            bool ok = BCrypt.Net.BCrypt.Verify(pswd, user.passwd);
            return ok ? true : false; // falls ok dann user falls !ok => false
        }
    }
}