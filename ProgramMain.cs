using TextVault.Services;

public class TextVaultMain
{
    public static void Main(string[] args)
    {
        // Console.WriteLine("Please enter your username: ");
        // string username = Console.ReadLine()!;
        // Console.WriteLine("\nPlease enter your password: ");
        // string password = Console.ReadLine()!;
        // AccountManager accountManager = new AccountManager();
        // accountManager.RegisterAccount(username, password);
        // if()
        string answer1 = String.Empty;
        string mainScreenText = "Please choose an action:\n[1] List all saved texts\n[2] Save a text\n[3] Delete a text\n[4] Register an account\n[5] Exit";
        do
        {
            Console.Clear();
            Console.WriteLine("Welcome to TEXTVAULT!\n\n");
            Console.WriteLine(mainScreenText);
            answer1 = Console.ReadLine()!;
            TextHandler textProvider = new TextHandler();

            if (answer1.Equals("1"))
            {
                Console.Clear();
                textProvider.ReadText();
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

                textProvider.SaveText(username, password, website, url);
                Console.WriteLine("\nText was saved successfully!");
                Thread.Sleep(2000);
            }
            if (answer1.Equals("3"))
            {
                Console.Clear();
                Console.WriteLine("\nPlease type in the text ID: ");
                string id = Console.ReadLine()!;
                if (id != null)
                {
                    textProvider.DeleteText(id);
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

                AccountManager accountManager = new AccountManager();
                accountManager.RegisterAccountJson(username, password);
                Console.WriteLine("\nText was saved successfully!");
            }
        }
        while (!answer1.Equals("5"));
    }
}