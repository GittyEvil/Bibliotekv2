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
        //kollar genom lista och ifall information stämmer loggas man in
        public static void LoginPage(bool UserisRenting)
        {
            string Data = File.ReadAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json");
            List<Person> UserList = JsonConvert.DeserializeObject<List<Person>>(Data)!;


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


                if (personnummer.ToString() == number && password == password1.ToString())
                {
                    User = user;
                    Program.MainPage(User, UserisRenting);
                    return;
                }

            }
        }
        //registrering som lägger till ny användare i json fil, läggs även till i lista för att kunna lätt hantera senare
        public static void RegisterPage(List<Person> UserList, bool UserisRenting)
        {
            
            if (UserList == null)
            {
                UserList = new List<Person>(); 
            }
            Console.WriteLine("skriv personnummer:");
            string personnummer1 = Console.ReadLine()!;
            Console.WriteLine("skriv ett lösenord");
            string lösenord1 = Console.ReadLine()!;
            Console.WriteLine("du har nu skapat ett konto.");

            Person newUser = new ("", "", Int32.Parse(personnummer1!), Int32.Parse(lösenord1!));

            
            UserList.Add(newUser);

            string dataToSave = JsonConvert.SerializeObject(UserList);
            File.WriteAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json", dataToSave);

            LoginPage(UserisRenting);

        }
        //hanterar konto uppgift ändringar(lösenords ändring)
        public static void UserInfoChanger(List<Person> UserList, Person User, List<Book> BookList)
        {
            bool UserisRenting = false;
            Console.WriteLine("Skulle du vilja byta lösenord?, 1:Ja , 2:Nej");
            string val = Console.ReadLine()!;

            if(val=="1")
            {
                Console.WriteLine("Skriv in ditt nya lösenord du skulle vilja ha, endast siffror");
                int newPassword = Int32.Parse(Console.ReadLine())!;
   
                //hittar användaren i userlistan(den som är inloggad)
                Person loggedInUser = UserList.FirstOrDefault(u => u.lösenord == User.lösenord)!;

                //ger nytt lösenord till användaren som hittats
                loggedInUser.lösenord = newPassword;

                string data = @"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\userAccounts.json";
                string json = JsonConvert.SerializeObject(UserList, Formatting.Indented);

                File.WriteAllText(data, json);
                
            }

            Program.MainPage(User, UserisRenting);
        }

    }
}
