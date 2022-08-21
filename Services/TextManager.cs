/* 
    This class is only used for reading and writing for a text file. 
    It is never used in this application and was only included for reference.
    All user inputs are now saved in a Json file.
*/

namespace TextVault.Services
{
    public class TextHandler
    {
        string filePath = "TextFile/SavedText.txt"; // Format for MacOS

        public void SaveText(string username, string password, string website, string url)
        {
            string randomID = GenerateID();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // Create a text file to write to.
            string toSave = $"ID: {randomID},Username: {username},Password: {passwordHash},Website: {website},URL: {url},-----,\n";
            File.AppendAllText(filePath, toSave);
        }

        public void ReadText()
        {
            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    // Resets position of pointer to the beginnning.
                    sr.DiscardBufferedData();
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);

                    string[] toRead = new string[5];
                    toRead = sr.ReadToEnd()!.Split(new char[] { '\n', ',' });
                    foreach (var item in toRead)
                    {
                        string itemToDisplay = item.Contains("-") ? item + "\n" : item;
                        Console.WriteLine(itemToDisplay);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No existing text. Please save a text first.");
            }
        }

        public void DeleteText(string id)
        {
            try
            {
                string tempFile = "TextFile/SavedTextTemp.txt";
                using (var sr = new StreamReader(filePath))
                using (var sw = new StreamWriter(tempFile))
                {
                    string line;
                    while ((line = sr.ReadLine()!) != null)
                    {
                        if (!line.Contains(id))
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                File.Delete(filePath);
                File.Move(tempFile, filePath);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No existing text to delete.");
                Thread.Sleep(1000);
            }
        }

        // Generates a random ID limited to length of 5
        public string GenerateID()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 5);
        }
    }
}