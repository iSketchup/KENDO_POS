using Main.ViewModels;
using Main.Models;

//var result1 = await Userhandling.VaidateLogin("Test", "hallo123");
//if (result1 != null)
//{
//    Console.WriteLine("Login erfolgreich");
//}

//var user = new User()
//{
//    UserName = "test",
//    passwd = "hallo123"
//};

//await Userhandling.AddUser(user.UserName, user.passwd);

//Userhandling.DeleteUser(user.UserName);
//Console.WriteLine("Wurde gelöscht");

await Userhandling.ChangeUser("test", "test12", "Passwort");
Console.WriteLine("Userdaten verändert");

bool result2 = await Userhandling.ValidateLogin("test","hallo123");
if (result2 != false)
{
    Console.WriteLine("Login erfolgreich");
    
} else
{
    Console.WriteLine("Fehlgeschlagen");
}

