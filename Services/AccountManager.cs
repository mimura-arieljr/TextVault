using System.Text.Json;

namespace TextVault.Services
{
    public class AccountManager
    {
        string filePath = "TextFile/SavedAccounts.json"; // Format for MacOS

        public void RegisterAccountJson(string username, string password)
        {
            Person person = new Person
            {
                username = username,
                password = password
            };

            // string fileName = "SavedAccounts.json";
            string jsonString = JsonSerializer.Serialize(person);
            File.AppendAllText(filePath,jsonString + Environment.NewLine);
        }
    }
    public class Person
    {
        public string? username {get; set;}
        public string? password {get; set;}
    }
}

