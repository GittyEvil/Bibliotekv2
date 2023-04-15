using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotektemp
{
    public class BookHandler
    {
        public class Handlebook
        {
            public static Person? LoggedInPerson;
            public static void Addbokpage()
            {
                Console.WriteLine("Vad är titeln för boken");
                string titel = Console.ReadLine()!;
                Console.WriteLine("vem heter författaren");
                string författare = Console.ReadLine()!;
                Console.WriteLine("vad är serienumret?");
                int serienummer = Int32.Parse((Console.ReadLine()!));
                Console.WriteLine("Hur många av boken vill du beställa?");
                int antal = Int32.Parse((Console.ReadLine()!));

                Book nybok = new Book(titel, antal, serienummer, författare);
                
            }

            //listar alla böcker där användaren senare kan låna en bok efter de valt vilken
            public static void ListAllbooks(Person User, bool UserisRenting)
            {
                string Data = File.ReadAllText(@"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\Books.json");
                List<Book> BookList = JsonConvert.DeserializeObject<List<Book>>(Data)!;
                string UserData = File.ReadAllText(@"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\userAccounts.json");
                List<Person> UserList = JsonConvert.DeserializeObject<List<Person>>(UserData)!;
                for (var i = 0; i < BookList.Count; i++)
                {
                    Book book = BookList[i];
                    Console.WriteLine($"{i + 1}.{book.Titel} {book.Författare} {book.Serienummer} {book.Antal}");
                    
                }

                Console.WriteLine("Skulle du vilja söka efter en specifik bok? 1:Ja, 2:Nej");
                string val = Console.ReadLine()!;

                if (val == "1")
                {
                    Console.WriteLine("Vilken bok? Du kan söka med författare och serienummer också.");
                    var choice = Console.ReadLine();
                    int number;

                    var isNumber = int.TryParse(choice, out number);

                    if (isNumber && number > 0 && number < BookList.Count + 1)
                    {
                        var chosenbook = BookList[number - 1];
                        SpecifikBookPage(User, chosenbook, BookList, UserList);
                    }

                }
                if (val == "2")
                {
                    Program.MainPage(User, UserisRenting);
                }
               
            }
            //sidan hanterar boken som användaren valt
            public static void SpecifikBookPage(Person User, Book book, List<Book> BookList, List<Person> UserList)
            {

                Console.WriteLine(book.Titel);
                Console.WriteLine(book.Författare);
                Console.WriteLine(book.Serienummer);
                Console.WriteLine(book.Antal);

                Console.WriteLine("Vad skulle du vilja göra nu?");
                Console.WriteLine("1. Gå tillbaka");
                bool UserisRenting = false;

                if (User.RentedBooks != null)
                {
                    foreach (Book Book in User.RentedBooks)
                    {
                        if (Book.Serienummer == book.Serienummer)
                        {
                            UserisRenting = true;
                            break;
                        }
                    }
                }


                if (UserisRenting == false)
                {
                    Console.WriteLine("2. Låna bok");

                }


                if (UserisRenting)
                {
                    Console.WriteLine("2. Lämna tillbaka bok");

                }

                Console.Write("Ditt val (1-2): ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    ListAllbooks(User, UserisRenting);
                }

                else if (choice == "2" && UserisRenting == false)
                {
                    RentBook(book, User, BookList, UserList,UserisRenting);
                    Program.MainPage(User, UserisRenting);
                }

                else if (choice == "2" && UserisRenting)
                {
                    ReturnBooks(book, User, BookList, UserList);
                    Program.MainPage(User, UserisRenting);
                }
            }
            //låna bok funktionen,tar reda på den inloggade användaren och lägger boken i dens lista
            static void RentBook(Book book, Person User, List<Book> BookList, List<Person> UserList, bool UserisRenting)
            {
                if (UserisRenting == false)
                {
                    //hittar användaren i userlistan(den som är inloggad)
                    Person loggedInUser = UserList.FirstOrDefault(u => u.id == User.id)!;

                    Book rentedBook = new(book.Titel!, book.Serienummer, book.Författare, book.Antal);

                    book.Ledig = false;

                    loggedInUser.RentedBooks.Add(rentedBook);

                    string data = @"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\userAccounts.json";
                    string json = JsonConvert.SerializeObject(UserList, Formatting.Indented);

                    File.WriteAllText(data, json);

                    Console.WriteLine($"Du har lånat boken '{book.Titel}'. Glöm inte att lämna tillbaka den senast om tre veckor.");
                }
            }
            //funktionen hanterar att söka upp en bok ifrån listan med alla böcker, det går att låna efter
            public static void SearchForBook(List<Book> BookList, List<Person> UserList, bool UserisRenting, Person User)
            {
                Console.WriteLine("Vad vill du söka efter? Du kan söka med titel, författare eller seriernummer:");
                string searchQuery = Console.ReadLine()!;
                //letar upp boken som man sökt efter, firstordefault loopar igenom listan och tar första bästa som matchar värdet/kraven
                Book book = BookList.FirstOrDefault(b => b.Titel?.ToLower() == searchQuery.ToLower() || b.Författare?.ToLower() == searchQuery.ToLower() || b.Serienummer.ToString() == searchQuery)!;

                if (book != null)
                {
                    Console.WriteLine($"Hittade följande bok: {book.Titel} av {book.Författare}, serienummer {book.Serienummer}");


                    if (UserisRenting == false)
                    {
                        Console.WriteLine("2. Låna bok");

                    }


                    if (UserisRenting)
                    {
                        Console.WriteLine("2. Lämna tillbaka bok");

                    }

                    Console.Write("Ditt val (1-2): ");
                    var choice = Console.ReadLine();
                    Console.WriteLine("1. Gå tillbaka");
                    if (choice == "1")
                    {
                        Program.MainPage(User, UserisRenting);
                    }

                    else if (choice == "2" && UserisRenting == false)
                    {
                        RentBook(book, User, BookList, UserList, UserisRenting);
                        Program.MainPage(User, UserisRenting);
                    }

                    else if (choice == "2" && UserisRenting)
                    {
                        ReturnBooks(book, User, BookList, UserList);
                        Program.MainPage(User, UserisRenting);
                    }
                }
                else
                {
                    Console.WriteLine("Boken kunde inte hittas.");
                }
            }

            //skapade denna för att spara den nya informationen/ändringen som skett, ska fixa så att den körs vid alla tillfällen över projektet
            public static void UpdateJson(List<Person> UserList, List<Book> BookList)
            {
                string jsonString = JsonConvert.SerializeObject(UserList, Formatting.Indented);
                File.WriteAllText(@"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\userAccounts.json", jsonString);

                jsonString = JsonConvert.SerializeObject(BookList, Formatting.Indented);
                File.WriteAllText(@"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\Books.json", jsonString);
            }

            public static void ReturnBooks(Book book, Person User, List<Book> BookList, List<Person> UserList)
            {
                //ska kolla om den inloggade användaren har lånat samma bok som man vill lämna tillbaka om det stämmer lämnar man tillbaka
                foreach (Book rentedBook in User.RentedBooks)
                {
                    if (rentedBook.Serienummer == book.Serienummer)
                    {
                        User.RentedBooks.Remove(rentedBook);
                        Console.WriteLine("Boken är nu återlämnad.");
                        book.Ledig = true;
                        //firstordefault letar efter den inloggade användaren i UserList för att sedan ta bort dess bok som den lånade
                        Person updatedUser = UserList.FirstOrDefault(u => u.personnummer == User.personnummer)!;
                        if (updatedUser != null)
                        {
                            updatedUser.RentedBooks = User.RentedBooks;
                            UpdateJson(UserList, BookList);
                            return;
                        }
                    }
                }
                Console.WriteLine("Kunde inte hitta boken i användarens hyrda böcker.");
            }


        }

    }
}
