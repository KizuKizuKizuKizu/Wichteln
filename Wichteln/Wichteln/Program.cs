using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.IO;
using Wichteln;
using CsvHelper;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;

namespace Wichteln
{
    class Program
    {       
        static void Main(string[] args)
        {
            Tabelle tab = new Tabelle();
            var rand = new Random();
            List<Personen> originallist = tab.getAll().ToList();
            List<Personen> shuffledList = tab.getAll().OrderBy(r => rand.Next()).ToList();
            List<Personen> assignedWichtelkinds = new List<Personen>(); // container 
            for (int i = 0; i < originallist.Count; i++)
            {
                Personen Wichtel = originallist[i];
                Personen Wichtelkind; 
                do
                {
                    shuffledList = shuffledList.OrderBy(r => rand.Next()).ToList(); // randomizer
                    Wichtelkind = shuffledList.First(p => p != Wichtel); // schaut ob es ungleich ist
                } 
                while (assignedWichtelkinds.Contains(Wichtelkind));               
                assignedWichtelkinds.Add(Wichtelkind);                
                Console.WriteLine($"{Wichtel.Vorname} {Wichtel.Nachname} hat als Wichtelkind {Wichtelkind.Vorname} {Wichtelkind.Nachname}"); // Wenn nur {Wichtel} kommen alle infos
            }

            try
            {
                foreach (var pair in assignedWichtelkinds.Zip(originallist, (k, v) => new { Wichtel = v, Wichtelkind = k }))
                {
                    SendMail(pair.Wichtel, pair.Wichtelkind);
                }
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
        public static void SendMail(Personen wichtel, Personen wichtelkind)
        {
            using (MailMessage newMail = new MailMessage())
            using (SmtpClient client = new SmtpClient("smtp.office365.com"))
            { 
                // Follow the RFS 5321 Email Standard
                newMail.From = new MailAddress("aldeschachtel5@outlook.de", "Alde");

                newMail.To.Add(wichtel.Email);//subject

                newMail.Subject = "My First Email"; // use HTML for the email body

                newMail.IsBodyHtml = true; newMail.Body = $"<h1> TEST!!!!</h1>" +
                              $"<p>{wichtel.Vorname} {wichtel.Nachname} du hast als Wichtelkind :  {wichtelkind.Vorname} {wichtelkind.Nachname}!</p>";                                                                          
                client.EnableSsl = true;
                // Port 465 for SSL communication
                client.Port = 587;
                // Provide authentication information with Gmail SMTP server to authenticate your sender account
                client.Credentials = new NetworkCredential("aldeschachtel5@outlook.de", "Digga123!"); // dummy mail

                client.Send(newMail); // Send the constructed mail
                Console.WriteLine("Email Sent");
            }           
        }    
    }
}
