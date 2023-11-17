 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Wichteln
{
    public class Personen
    {
        public int Nummer { get; set; } 
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return Nummer + " " + Vorname + " " + Nachname + " " + Email;
        }
    }
}
