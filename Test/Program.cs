using Main.ViewModels;


Console.WriteLine("Hello, World!");

var result1 = await Userhandling.VaidateLogin("Daniel", "123HTL12");
if (result1 != null)
{
    Console.WriteLine("Login erfolgreich");
}

var result2 = await Userhandling.VaidateLogin("Walser", "DanHTLWal");
if (result2 != null)
{
    Console.WriteLine("Login erfolgreich");
} else
{
    Console.WriteLine("Fehlgeschlagen");
}