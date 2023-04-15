using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bibliotektemp
{
    internal class Program
    {

        public static Person? loggedInPerson;
        public static string userData = File.ReadAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json");
        public static void Main(string[] args)
        {
            string Data = File.ReadAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json");
            List<Person> UserList = JsonConvert.DeserializeObject<List<Person>>(Data)!;

            string userData = File.ReadAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json");
            //dynamic personData = JsonConvert.DeserializeObject<dynamic>(Data)!;
            List<Book> BookList = JsonConvert.DeserializeObject<List<Book>>(userData)!;
            bool UserisRenting = false;
            Console.WriteLine("hej och välkommen till Biblioteket i väst");
            Console.WriteLine("Vad skulle du vilja göra?");
            Console.WriteLine("1. Skapa konto, 2. Logga in");

            string val = Console.ReadLine()!;

            if (val == "1")
            {
                AccountHandler.RegisterPage(UserList, UserisRenting);
            }

            if (val == "2")
            {
                AccountHandler.LoginPage(UserisRenting);
            } 
        }

       
        public static void LoggedUser()
        {
            dynamic personData = JsonConvert.DeserializeObject<dynamic>(userData)!;
            foreach (var i in personData)
            {
                loggedInPerson = new Person((int)i.personnummer, (int)i.lösenord);
            }
        }
        public static void MainPage(Person User, bool UserisRenting)
        {
            string Data = File.ReadAllText(@"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\Books.json");
            List<Book> BookList = JsonConvert.DeserializeObject<List<Book>>(Data)!;
            string UserData = File.ReadAllText(@"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\userAccounts.json");
            List<Person> UserList = JsonConvert.DeserializeObject<List<Person>>(UserData)!;
            Console.WriteLine("Nu är du inloggad");
            Console.WriteLine("Vad vill du göra nu?");
            Console.WriteLine("1. Lista böcker,2. Söka böcker,3. Ändra kontouppgifter ,4. Logga ut");

            string val = Console.ReadLine()!;

            if(val == "1")
            {
                BookHandler.Handlebook.ListAllbooks(User, UserisRenting);

            }
            if (val == "2")
            {
                //funktionen fungerar men har inte löst hur jag ska köra den.
                BookHandler.Handlebook.SearchForBook(BookList, UserList, UserisRenting, User);
            }
            if (val == "3")
            {
                AccountHandler.UserInfoChanger(UserList, User, BookList);
            }
            if (val == "4")
            {

            }

        }
        
    }
}