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


            public static void ListAllbooks()
            {
                string Data = File.ReadAllText(@"C:\Users\adria\Documents\Bibliotektemp\Bibliotektemp\Books.json");
                List<Book> BookList = JsonConvert.DeserializeObject<List<Book>>(Data)!;
                for(var i = 0; i < BookList.Count; i++)
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
                        SpecifikBookPage(chosenbook);
                    }
                    
                }
                if (val == "2")
                {
                    Program.MainPage();
                }
            }

            static void SpecifikBookPage(Book book)
            {
                Console.WriteLine(book.Titel);
                Console.WriteLine(book.Författare);
                Console.WriteLine(book.Serienummer);
                Console.WriteLine(book.Antal);

                Console.WriteLine("Vad skulle du vilja göra nu?");
                Console.WriteLine("1. Gå tillbaka");

                //bool userIsRenting = System.currentPersonLoaningBook(book);
                bool UserisRenting = false;
                /*
                if (Person.RentedBooks != null)
                {
                    foreach (Book Book in Person.personnummer)
                    {
                        if (Book.Serienummer == LoggedInPerson.personnummer)
                        {
                            UserisRenting = true;
                            break;
                        }
                    }
                }
                */

                if (book.Ledig)
                {
                    Console.WriteLine("2. Låna bok");

                }


                //if (userIsRenting)
                {
                    Console.WriteLine("2. Lämna tillbaka bok");

                }

                Console.Write("Ditt val (1-2): ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    ListAllbooks();
                }

                //else if (choice == "2" && bok.Ledig)
                {
                    //System.Lånabok(bok);

                    Lånabok();
                }

                //else if (choice == "2" && userIsRenting)
                {
                    //System.Returnbooks(bok);
                    LämnatillbakaBöcker();
                }
            }

            }
            static void Lånabok()
            {
                Console.WriteLine("bok är nu lånad");
                //Listaböcker();
            }

            void ListaLånadeBöcker()
            {
                //fixa så man kan lista lånade böcker
                Console.WriteLine("");
            }


            public static void LämnatillbakaBöcker()
            {
                //lämna tillbaka böcker
                Console.WriteLine("boken är nu återlämnad");
                //Listaböcker();
            }
                
        }
    }
