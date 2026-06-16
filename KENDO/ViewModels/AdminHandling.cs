using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
    public static class AdminHandling
    {
        // Nutzt denselben HttpClient oder ApiService wie dein Userhandling
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


        // Holt alle User, gibt aber NUR die Benutzernamen zurück.
        public async static Task<List<string>> GetAllUserNames()
        {
            List<User> allUsers = await apiService.GetAllUsers();


            return allUsers
                .Where(u => !string.IsNullOrEmpty(u.UserName))
                .Select(u => u.UserName!)
                .ToList();
        }

        public async static Task<bool> AddAdmin(string name, string pswd)
        {
            if (string.IsNullOrWhiteSpace(pswd) || pswd.Length < 8)
                return false;

            // 1. Passwort hashen (genau wie beim User)
            string hashed = BCrypt.Net.BCrypt.HashPassword(pswd);

            // 2. User-Objekt bauen
            User newAdmin = new User { UserName = name, passwd = hashed, is_admin = true };

            await apiService.CreateAdmin(newAdmin);

            return true;
        }

        // Holt alle Benutzernamen, die einen bestimmten Suchbegriff enthalten.
        public async static Task<List<string>> FilterUsersByName(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllUserNames();
            }

            List<User> allUsers = await apiService.GetAllUsers();

            return allUsers
                .Where(u => u.UserName != null && u.UserName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .Select(u => u.UserName!)
                .ToList();
        }


        // Löscht einen User anhand seines Namens.
        public async static Task<bool> DeleteUserByAdmin(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            // Nutzt die bestehende Löschlogik deiner API
            await apiService.DeleteUser(username);
            return true;
        }
    }
}
