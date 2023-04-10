﻿using Newtonsoft.Json;
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
                //System.AddBok(nybok);
            }


            public static void ListAllbooks(Person User)
            {
                string Data = File.ReadAllText(@"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\Books.json");
                List<Book> BookList = JsonConvert.DeserializeObject<List<Book>>(Data)!;
                string UserData = File.ReadAllText(@"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\userAccounts.json");
                List<Person> UserList = JsonConvert.DeserializeObject<List<Person>>(UserData)!;
                for (var i = 0; i < BookList.Count; i++)
                {
                    Book book = BookList[i];
                    Console.WriteLine($"{i + 1}.{book.Titel} {book.Författare} {book.Serienummer} {book.Antal}");
                    book.Ledig = true;
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
                    //Program.MainPage();
                }
            }

            public static void SpecifikBookPage(Person User, Book book, List<Book> BookList, List<Person> UserList)
            {
                Console.WriteLine(book.Titel);
                Console.WriteLine(book.Författare);
                Console.WriteLine(book.Serienummer);
                Console.WriteLine(book.Antal);

                Console.WriteLine("Vad skulle du vilja göra nu?");
                Console.WriteLine("1. Gå tillbaka");

                //bool userIsRenting = System.currentPersonLoaningBook(book);
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
                

                if (book.Ledig)
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
                    ListAllbooks(User);
                }

                else if (choice == "2" && book.Ledig)
                {
                    Lånabok(book, User, BookList, UserList);
                }

                else if (choice == "2" && UserisRenting)
                {
                    //System.Returnbooks(bok);
                    LämnatillbakaBöcker(book, User, BookList, UserList);
                }
            }

            }
        static void Lånabok(Book book, Person user, List<Book> bookList, List<Person> userList)
        {
            if (book.Ledig)
            {
                //hittar användaren i userlistan(den som är inloggad)
                Person loggedInUser = userList.FirstOrDefault(u => u.id == user.id)!;

                Book rentedBook = new (book.Titel!,book.Serienummer);

                book.Ledig = false;

                loggedInUser.RentedBooks.Add(rentedBook);

                string data = @"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\userAccounts.json";
                string json = JsonConvert.SerializeObject(userList, Formatting.Indented);

                File.WriteAllText(data, json);

                Console.WriteLine($"Du har lånat boken '{book.Titel}'. Glöm inte att lämna tillbaka den senast om tre veckor.");
            }
        }
        void ListaLånadeBöcker()
            {
                //fixa så man kan lista lånade böcker
                Console.WriteLine("");
            }


            public static void LämnatillbakaBöcker(Book book, Person user, List<Book> bookList, List<Person> userList)
            {
                
                Console.WriteLine("boken är nu återlämnad");
                
            }
                
        }
    }
