using Main.ViewModels;
using Main.Models;

Console.WriteLine("Hello, World!");

var result1 = await Userhandling.VaidateLogin("Daniel", "123HTL12");
if (result1 != null)
{
    Console.WriteLine("Login erfolgreich");
}

var user = new User()
{
    UserName = "Test",
    passwd = "Hallo123"
};

await Userhandling.AddUser(user.UserName, user.passwd);

var result2 = await Userhandling.VaidateLogin("Walser", "DanHTLWal");
if (result2 != null)
{
    Console.WriteLine("Login erfolgreich");
} else
{
    Console.WriteLine("Fehlgeschlagen");
}