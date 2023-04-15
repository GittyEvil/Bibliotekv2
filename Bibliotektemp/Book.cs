using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotektemp
{
    public class Book
    {
        public string? Titel { get; set; }
        public int? Serienummer { get; set; }
        public int? Antal { get; set; }
        public string? Författare { get; set; }
        public bool Ledig { get; set; }

        public Book()
        {
            
        }
        public Book(string bokTitel, int bokAntal, int bokSerienummer, string bokFörfattare, bool bokLedig = true)
        {
            this.Titel = bokTitel;
            this.Antal = bokAntal;
            this.Serienummer = bokSerienummer;
            this.Författare = bokFörfattare;
            this.Ledig = bokLedig;
        }

        public Book(string titel, int serienummer, string författare)
        {
            Titel = titel;
            Serienummer = serienummer;
            Författare = författare;
        }
        public Book(string titel)
        {
            Titel = titel;
        }
        public Book(string titel, int? serienummer, string? författare, int? antal)
        {
            Titel = titel;
            Serienummer = serienummer;
            Författare = författare;
            Antal = antal;
        }
    }
}
