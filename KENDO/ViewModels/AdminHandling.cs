using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Main.ViewModels
{
    public static class AdminHandling
    {
        
        private static HttpClient client = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(15),
            SslOptions = new System.Net.Security.SslClientAuthenticationOptions
            {
                RemoteCertificateValidationCallback = (msg, cert, chain, errors) => true
            }
        })
        {
            BaseAddress = new Uri("https://127.0.0.1:8000")
        };

        private static ApiService apiService = new ApiService(client);


        
        public async static Task<List<string>> GetAllUserNames()
        {
            List<User> allUsers = await apiService.GetAllUsers();

            // **** KI Teil
            // Hier habe ich gefragt, wie ich am besten alle User gefiltert nach dem Namen zurückgeben kann.
            return allUsers
                .Where(u => !string.IsNullOrEmpty(u.UserName))
                .Select(u => u.UserName!)
                .ToList();
            // ****
        }

        public async static Task<bool> AddAdmin(string name, string pswd)
        {
            if (string.IsNullOrWhiteSpace(pswd) || pswd.Length < 8)
                return false;

            
            string hashed = BCrypt.Net.BCrypt.HashPassword(pswd);

            
            User newAdmin = new User { UserName = name, Passwd = hashed, is_admin = true };

            await apiService.CreateAdmin(newAdmin);

            return true;
        }

        /* Ist im AdminViewModel selbst implementiert
        public async static Task<bool> DeleteUserByAdmin(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            
            await apiService.DeleteUser(username);
            return true;
        }*/
    }
}
