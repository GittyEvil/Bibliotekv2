using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotektemp
{
   

    public class AccountHandler

    {
        public static Person LoggedInPerson = null!;
        public static void LoginPage()
        {
            string Data = File.ReadAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json");
            List<Person> UserList = JsonConvert.DeserializeObject<List<Person>>(Data)!;

            //Person user = null;

            Console.WriteLine("Logga in:");
            Console.WriteLine("Personnummer:");
            string number = Console.ReadLine()!;
            Console.WriteLine("Lösenord:");
            string password = Console.ReadLine()!;

            Person User = null;

            foreach(Person user in UserList)
            {
                var personnummer = user.personnummer;
                var password1 = user.lösenord;

                //LoggedInPerson = new Person(personnummer.ToString(), password1.ToString());

                if (personnummer.ToString() == number && password == password1.ToString())
                {
                    Program.MainPage(User);
                    User = user;
                    return;
                }

            }
        }

        public static void RegisterPage(List<Person> UserList)
        {
            //string Data = File.ReadAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json");
            //dynamic personData = JsonConvert.DeserializeObject<dynamic>(Data)!;
            //List<Person> UserList = JsonConvert.DeserializeObject<List<Person>>(Data)!;
            if (UserList == null)
            {
                UserList = new List<Person>(); // Initialize UserList if it is null
            }
            Console.WriteLine("skriv personnummer:");
            string personnummer1 = Console.ReadLine()!;
            Console.WriteLine("skriv ett lösenord");
            string lösenord1 = Console.ReadLine()!;
            Console.WriteLine("du har nu skapat ett konto.");

            Person newUser = new ("", "", Int32.Parse(personnummer1!), Int32.Parse(lösenord1!));

            /*
            if (personData == null)
            {
                personData = new JArray();
            }
            personData.Add(JToken.FromObject(newUser));
            */
            UserList.Add(newUser);

            string dataToSave = JsonConvert.SerializeObject(UserList);
            File.WriteAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json", dataToSave);

            LoginPage();

        }

        

    }
}
