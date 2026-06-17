using BCrypt.Net;
using Main.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
        if (pswd == null)
            return false;

        if (pswd.Length < 8)
            return false;
        
        
        User? gettingUser = await apiService.GetUserInfo(name, pswd);

        if (gettingUser.UserName == name)
        {
            return false;
        }

        string hashed = BCrypt.Net.BCrypt.HashPassword(pswd);
        User user =  new User { UserName = name, Passwd = hashed };
        

        await apiService.CreateUser(user);


        return true;
    }



    public async static Task<bool> ChangeUser(string username, string name, string newpswd)
    {
        string PasswordToSend;
        
        if (newpswd == "")
        {
            PasswordToSend = "";
        }
        else
        {
            if (newpswd.Length < 8)
            {
                return false;
            }

            PasswordToSend = BCrypt.Net.BCrypt.HashPassword(newpswd);
        }


        User updatedUserPass = new User { UserName = name, Passwd = PasswordToSend };


        await apiService.ChangeUser(username, updatedUserPass);
        return true;
    }



    public async static Task<bool> DeleteUser(string username)
    {
        // Einfach nur nach den Namen suchen und dann diesen User löschen.
        // Sehr simple
        if (username == null)
            return false;
        
        await apiService.DeleteUser(username);
        return true;
    }



    public async static Task<User?> ValidateLogin(string name, string pswd)
    {
        User? user = await apiService.GetLogin(name, pswd);

        // Ist ein Passwort vorhanden?
        //if (user == null || string.IsNullOrWhiteSpace(user.passwd)) return null;
        //else
        //{
        //    Log.Debug($"API-Response erhalten. User: {user.UserName}, IsAdmin im Frontend: {user.is_admin}");
        //    bool ok = BCrypt.Net.BCrypt.Verify(pswd, user.passwd);
        //    return ok ? user : null; // falls ok dann user falls !ok => false
        //}

        if (user == null)
        {
            Log.Warning($"Login fehlgeschlagen für User: {name}");
            return null;
        }

        Log.Debug($"API-Response erhalten. User: {user.UserName}, IsAdmin im Frontend: {user.is_admin}");

        // Einfach den User zurückgeben, da das Backend ihn bereits verifiziert hat
        return user;
    }
}