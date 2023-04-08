using Newtonsoft.Json;

namespace Bibliotektemp
{
    internal class Program
    {

        public static Person? loggedInPerson;
        public string userData = File.ReadAllText("C:\\Users\\adria\\Documents\\Bibliotektemp\\Bibliotektemp\\userAccounts.json");
        static void Main(string[] args)
        {
            FirstPage();
        }

        static void FirstPage()
        {
            Console.WriteLine("hej och välkommen till Biblioteket i väst");
            Console.WriteLine("Vad skulle du vilja göra?");
            Console.WriteLine("1. Skapa konto, 2. Logga in");

            string val = Console.ReadLine()!;

            if (val == "1")
            {
                AccountHandler.RegisterPage();
            }

            if (val == "2")
            {
                AccountHandler.LoginPage();
            }

        }
        public void LoggedUser()
        {
            dynamic personData = JsonConvert.DeserializeObject<dynamic>(userData)!;
            foreach (var i in personData)
            {
                loggedInPerson = new Person((int)i.personnummer, (int)i.lösenord);
            }
        }
        public static void MainPage()
        {
            string bookData = File.ReadAllText(@"C:\Users\adria\Downloads\ConsoleApp1\ConsoleApp1\ConsoleApp1\Books.json");
            List<Book> BookList = JsonConvert.DeserializeObject<List<Book>>(bookData)!;
            Console.WriteLine("Nu är du inloggad");
            Console.WriteLine("Vad vill du göra nu?");
            Console.WriteLine("1. Lista böcker,2. Söka böcker,3. Ändra kontouppgifter ,4. Logga ut");

            string val = Console.ReadLine()!;

            if(val == "1")
            {
                BookHandler.Handlebook.ListAllbooks();
            }
            if (val == "2")
            {

            }
            if (val == "3")
            {

            }
            if (val == "4")
            {

            }

        }
        
    }
}