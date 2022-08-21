using TextVault.Services;

public class TextVaultMain
{
    public static void Main(string[] args)
    {
        bool accountChecker = false;
        do
        {
            Console.Clear();
            Console.WriteLine("Please enter your username: ");
            string acctUsername = Console.ReadLine()!;
            Console.WriteLine("\nPlease enter your password: ");
            string acctPassword = Console.ReadLine()!;
            AccountManager accountManager = new AccountManager();
            if (accountManager.VerifyAccountJson(acctUsername, acctPassword))
            {
                accountChecker = true;
                string answer1 = String.Empty;
                string mainScreenText = "Please choose an action:\n[1] List all saved texts\n[2] Save a text\n[3] Delete a text\n[4] Register an account\n[5] Exit";
                do
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to TEXTVAULT!\n\n");
                    Console.WriteLine(mainScreenText);
                    answer1 = Console.ReadLine()!;
                    JsonManager jsonManager = new JsonManager();

                    if (answer1.Equals("1"))
                    {
                        Console.Clear();
                        jsonManager.ReadJsonText();
                        Console.ReadLine();
                    }
                    if (answer1.Equals("2"))
                    {
                        Console.Clear();
                        Console.WriteLine("Please type in the username: ");
                        string username = Console.ReadLine()!;
                        Console.WriteLine("\nPlease type in the password: ");
                        string password = Console.ReadLine()!;
                        Console.WriteLine("\nPlease type in the Website name: ");
                        string website = Console.ReadLine()!;
                        Console.WriteLine("\nPlease type in the URL. Please leave NA if you choose not to answer.");
                        string url = Console.ReadLine()!;

                        jsonManager.SaveJsonText(username, password, website, url);
                        Console.WriteLine("\nText was saved successfully!");
                        Thread.Sleep(2000);
                    }
                    if (answer1.Equals("3"))
                    {
                        Console.Clear();
                        Console.WriteLine("\nPlease type in the text username: ");
                        string id = Console.ReadLine()!;
                        if (id != String.Empty)
                        {
                            jsonManager.DeleteJsonText(id);
                            Console.WriteLine("\nText deleted successfully!");
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.WriteLine("Empty input. Please try again.");
                            Thread.Sleep(1000);
                        }
                    }
                    if (answer1.Equals("4"))
                    {
                        Console.Clear();
                        Console.WriteLine("Please type in the username you want to register: ");
                        string username = Console.ReadLine()!;
                        Console.WriteLine("\nPlease type in your password: ");
                        string password = Console.ReadLine()!;

                        accountManager.RegisterAccountJson(username, password);
                        Console.WriteLine("\nText was saved successfully!");
                    }
                }
                while (!answer1.Equals("5"));
            }
            else
            {
                Console.WriteLine("\nUsername and/or password is incorrect. Please try again.");
                Thread.Sleep(1500);
            }
        }
        while (!accountChecker);
    }
}