using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotektemp
{
    public class Person
    {
            public string? förnamn { get; set; }
            public string? efternamn { get; set; }
            public string? id { get; set; }
            public int? personnummer { get; set; }
            public int? lösenord { get; set; }

            public List<Book> RentedBooks = new List<Book>();
            public Person()
            {
                
            }
            public Person(string förnamn, string id) 
            {
                this.förnamn = förnamn;
                this.id = id;
                
            }

            public Person(string förnamn, string efternamn, int personnummer, int lösenord) 
            {
                this.förnamn = förnamn;
                this.efternamn = efternamn;
                this.personnummer = personnummer;
                this.lösenord = lösenord;
            }

            public Person(int personnummer, int lösenord)
            {
                this.personnummer = personnummer;
                this.lösenord = lösenord;
            }
        
    }
}
