using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Wichteln
{
    class Tabelle
    {
        private List<Personen> liste;

        public Tabelle() 
        {
            liste = new List<Personen>();
            string[] zeilen = File.ReadAllLines(@"C:\Users\amiller\OneDrive - quibiq GmbH\Desktop\Teilnehmer.csv");
            foreach (string zeile in zeilen)
            {
                string[] daten = zeile.Split(';');
                int nummer = int.Parse(daten[0]);
                string Vorname = daten[1];
                string Nachname = daten[2];
                string email = daten[3];

                liste.Add(new Personen { Nummer = nummer, Vorname = Vorname, Nachname = Nachname, Email = email});
            }
        }
        public Personen[] getAll()
        {
            return liste.ToArray();
        }
        public void Hinzufügen(int nummer, string vorname, string nachname, string email)  //ALS TEST 
        {
            liste.Add(new Personen { Nummer = nummer, Vorname = vorname, Nachname = nachname, Email = email });
        }
        ~Tabelle()
        {
            string[] daten = new string[liste.Count];
            for (int i = 0; i < daten.Length; i++)
            {
                daten[i] = liste[i].Nummer + ";" + liste[i].Vorname + ";"+ liste[i].Nachname + ";"+ liste[i].Email + ";";
            }
            File.WriteAllLines(@"C:\Users\amiller\OneDrive - quibiq GmbH\Desktop\Teilnehmer.csv", daten);
        }

    }
}
