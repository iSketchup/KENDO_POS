using System.Threading.Tasks;
using Xunit;
using Main.ViewModels;
using Main.Models;

namespace Main.Tests
{
    public class UserhandlingTests
    {
        // Prüft ob das Login funktioniert
        [Fact]
        public async Task VaidateLogin_ExistingUser_ReturnsNotNull()
        {

            bool result = await Userhandling.ValidateLogin("Daniel", "123HTL12");


            Assert.NotNull(result);
        }


        [Fact]
        public async Task VaidateLogin_UnknownUser_ReturnsNull()
        {

            bool result = await Userhandling.ValidateLogin("Walser", "DanHTLWal");


            Assert.False(result);
        }


        // Test das Löschen eines Users
        // (Besonders bevor die anderen Tests gestartet werden)
        [Fact]
        public async Task Delete_Existing_User()
        {
            User user = new User
            {
                UserName = "Test",
                passwd = "Hallo123"
            };

            bool deleted = await Userhandling.DeleteUser(user.UserName);


            Assert.True(deleted);
        }


        // Schaut ob ein neuer User erstellt werden kann.
        [Fact]
        public async Task AddUser_NewUser_DoesNotThrow()
        {

            User user = new User
            {
                UserName = "Test",
                passwd = "Hallo123"
            };


            
            bool added = await Userhandling.AddUser(user.UserName, user.passwd);


            Assert.True(added);
        }


        // Testet das Bearbeiten eines Users
        [Fact]
        public async Task ChangeUser_OldUsername_NoLongerValid()
        {
            string originalUsername = "test";
            string newUsername = "test12";
            string password = "hallo123";

            await Userhandling.AddUser(originalUsername, password);

            try
            {
                await Userhandling.ChangeUser(originalUsername, newUsername, password);
                bool loginWithOldName = await Userhandling.ValidateLogin(originalUsername, password);


                Assert.False(loginWithOldName);
            }
            finally
            {
                Userhandling.DeleteUser(newUsername);
            }
        }
    }
}