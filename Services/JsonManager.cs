using Newtonsoft.Json;

namespace TextVault.Services
{
    public class JsonManager
    {
        string filePathJson = "TextFile/SavedTexts.json";
        RootObject rootObject = new RootObject();
        public void SaveJsonText(string username, string password, string website, string url)
        {
            string uniqueID = GenerateID();
            if (!File.Exists(filePathJson))
            {
                rootObject.texts = new List<SavedText>{
                new SavedText {uniqueID = uniqueID, username = username, password = password, website = website, url = url}};
                string serializedJsonResult = JsonConvert.SerializeObject(rootObject);
                using (var st = new StreamWriter(filePathJson))
                {
                    st.WriteLine(serializedJsonResult.ToString());
                    st.Close();
                }
            }
            else // Append the new saved "text" to json file if it exists.
            {
                // Opens the json file and stores json string contents to reader.
                StreamReader reader = new StreamReader (filePathJson);
                string serializedJsonResult = reader.ReadToEnd();
                reader.Close();              
                // Deserializes json string to object.
                RootObject appendOnList = JsonConvert.DeserializeObject<RootObject>(serializedJsonResult);
                appendOnList.texts.Add(new SavedText {uniqueID = uniqueID, username = username, password = password, website = website, url = url });
                string serializedJsonAppended = JsonConvert.SerializeObject(appendOnList);
                using (var st = new StreamWriter(filePathJson))
                {
                    st.WriteLine(serializedJsonAppended.ToString());
                    st.Close();
                }
            }
        }

        public void ReadJsonText()
        {
            if (File.Exists(filePathJson))
            {
                using (var sr = new StreamReader(filePathJson))
                {
                    var serializedJson = sr.ReadToEnd();
                    RootObject listOfText = JsonConvert.DeserializeObject<RootObject>(serializedJson);
                    foreach (var item in listOfText.texts)
                    {
                        Console.WriteLine($"--------\nID: {item.uniqueID} \nUsername: {item.username} \nPassword: {item.password} \nWebsite: {item.website} \nURL: {item.url} \n ");
                        sr.Close();
                    }
                }
            }
            else
            {
                Console.WriteLine("No existing text. Please save a new one.");
            }
        }

        public void DeleteJsonText(string ID)
        {
            using (var sr = new StreamReader(filePathJson))
            {
                var serializedJson = sr.ReadToEnd();
                sr.Close();
                RootObject listOfText = JsonConvert.DeserializeObject<RootObject>(serializedJson);
                SavedText itemToRemove = null;
                foreach(var item in listOfText.texts)
                {
                    if(item.uniqueID == ID)
                    {itemToRemove = item;}
                }
                listOfText.texts.Remove(itemToRemove);

                string serializedJsonReduced= JsonConvert.SerializeObject(listOfText);
                using (var st = new StreamWriter(filePathJson))
                {
                    st.WriteLine(serializedJsonReduced.ToString());
                    st.Close();
                }   
            }
        }

        // Generates a random ID limited to length of 5
        public string GenerateID()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 5);
        }
    }

    public class SavedText
    {
        public string uniqueID;
        public string username;
        public string password;
        public string website;
        public string url;
    }
    public class RootObject
    {
        public List<SavedText> texts;
    }
}