using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Main.Models;
using Serilog;

namespace Main.ViewModels;

public partial class AdminViewModel: ViewModelBase
{
    private AppContext appContext;
    private readonly NavigationService _navigation;

    // Die Liste, an die die Avalonia-UI (ListBox) gebunden wird
    [ObservableProperty]
    private ObservableCollection<User> _users = new();

    public AdminViewModel(NavigationService navigation)
    {
        _navigation = navigation;
    }
    
    public async Task UpdateContexts(AppContext appContext)
    {
        this.appContext = appContext;
        
        
        await LoadAllUsers();
    }
    
    [RelayCommand]
    public async Task DeleteUserCommand(User user)
    {
        if (user == null) return;

        
        Log.Information($"Löschbefehl erhalten für User: {user.UserName}");
        bool success = await Userhandling.DeleteUser(user.UserName);

        
        Log.Information($"API-Löschstatus für {user.UserName}: {success}");
        if (success)
        {
            
            Users.Remove(user);
        }
    }

    public async Task LoadAllUsers()
    {
        // Liefert eine List<string> (z.B. ["Admin", "Daniel", "TestUser"])
        List<string> loadedUserNames = await AdminHandling.GetAllUserNames();
        
        Users.Clear();
        foreach (string name in loadedUserNames)
        {
            // Hier wird aus dem String ein echtes User-Objekt gebaut:
            User neuerUser = new User { UserName = name };
        
            Users.Add(neuerUser); 
        }
    }
}