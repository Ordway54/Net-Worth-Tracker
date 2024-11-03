using System.Text.Json;
using System.IO;

namespace NetWorthTracker
{
    internal static class UserConfigManager
    {
        // manages the User Configuration file

        public static string[] GetUserAccountNames()
        {
            string jsonFilePath = @"C:\\Users\\xxrus\\OneDrive\\Documents\\Programming\\C# Net Worth Tracker\\assets\userconfig.json";
            string jsonContent = File.ReadAllText(jsonFilePath);
            UserConfig? userConfig = JsonSerializer.Deserialize<UserConfig>(jsonContent);

            if (userConfig != null)
            {
                Console.WriteLine(userConfig.ToString());
            }

            return userConfig.Accounts;
        }
    }

    internal class UserConfig
    {
        public string FirstName {  get; private set; }
        public string LastName { get; private set; }
        public string[] Accounts { get; private set; }

        public UserConfig(string firstName, string lastName, string[] accounts)
        {
            FirstName = firstName;
            LastName = lastName;
            Accounts = accounts;
        }

        public string ToString()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}, Accounts: {Accounts.ToString()}";
        }

    }
}
