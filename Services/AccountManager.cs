using Newtonsoft.Json;

namespace TextVault.Services
{
    public class AccountManager
    {
        string filePath = "TextFile/SavedAccounts.json"; // Format for MacOS

        public void RegisterAccountJson(string username, string password)
        {
            PersonList personList = new PersonList();
            if (!File.Exists(filePath))
            {
                personList.person = new List<Person>{
                new Person {username = username, password = password}};
                string serializedJsonResult = JsonConvert.SerializeObject(personList);
                using (var st = new StreamWriter(filePath))
                {
                    st.WriteLine(serializedJsonResult.ToString());
                    st.Close();
                }
            }
            else
            {
                StreamReader reader = new StreamReader(filePath);
                string serializedJsonResult = reader.ReadToEnd();
                PersonList appendOnList = JsonConvert.DeserializeObject<PersonList>(serializedJsonResult);
                appendOnList.person.Add(new Person { username = username, password = password });
                string serializedJsonAppended = JsonConvert.SerializeObject(appendOnList);
                using (var st = new StreamWriter(filePath))
                {
                    st.WriteLine(serializedJsonAppended.ToString());
                    st.Close();
                }
            }
        }

        public bool VerifyAccountJson(string username, string password)
        {
            bool validate = false;
            StreamReader reader = new StreamReader(filePath);
            string serializedJsonResult = reader.ReadToEnd();
            PersonList personList = JsonConvert.DeserializeObject<PersonList>(serializedJsonResult);
            Person verifiedUser = new Person {username = username, password = password};
            // validate = personList.person.Contains(verifiedUser) ? true : false;
            foreach (var item in personList.person)
            {
                if(item.username == username)
                {
                    validate = item.password == password ? true : false;
                }
            }
            return validate;
        }
        public class Person
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class PersonList
        {
            public List<Person> person { get; set; }
        }
    }
}

