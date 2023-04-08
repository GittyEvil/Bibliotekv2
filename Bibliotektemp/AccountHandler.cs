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
        public string userData = File.ReadAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json");
        public static void LoginPage()
        {
            string Data = File.ReadAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json");
            dynamic userData = JsonConvert.DeserializeObject<dynamic>(Data)!;
            //Person user = null;

            Console.WriteLine("Logga in:");
            Console.WriteLine("Personnummer:");
            string number = Console.ReadLine()!;
            Console.WriteLine("Lösenord:");
            string password = Console.ReadLine()!;



            foreach(var i in userData)
            {
                var personnummer = (string)i["personnummer"];
                var password1 = (string)i["lösenord"];

                LoggedInPerson = new Person(personnummer, password1);

                if (personnummer == number && password == password1)
                {
                    Program.MainPage();
                    //LoggedInPerson();
                    return;
                }

            }
        }

        public static void RegisterPage()
        {
            string Data = File.ReadAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json");
            dynamic personData = JsonConvert.DeserializeObject<dynamic>(Data)!;
            Console.WriteLine("skriv personnummer:");
            string personnummer1 = Console.ReadLine()!;
            Console.WriteLine("skriv ett lösenord");
            string lösenord1 = Console.ReadLine()!;
            Console.WriteLine("du har nu skapat ett konto.");

            Person newUser = new ("", "", Int32.Parse(personnummer1!), Int32.Parse(lösenord1!));

            if (personData == null)
            {
                personData = new JArray();
            }
            personData.Add(JToken.FromObject(newUser));

            string dataToSave = JsonConvert.SerializeObject(personData);
            File.WriteAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json", dataToSave);

            LoginPage();

        }

        

    }
}
