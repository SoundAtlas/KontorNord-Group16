using System; // Giver adgang til grundlæggende funktioner i C#, fx Console og DateTime.
using System.Collections.Generic; // Giver adgang til generiske lister, fx List<T>.
using System.Globalization; // Giver adgang til kultur- og dato/tidsformater, som bruges til validering.

namespace KontorNordBooking // Navnerum der samler programmets klasser under samme projekt.
{
    internal class Program // Hovedklassen i programmet. Her starter programmet.
    {
        static void Main(string[] args) // Main-metoden er programmets startpunkt.
        {
            BookingSystem system = new BookingSystem();
            // Opretter et objekt af klassen BookingSystem.
            // Dette objekt styrer oprettelse, visning, redigering og sletning af bookinger.

            List<Medarbejder> medarbejdere = new List<Medarbejder>()
            // Opretter en liste med medarbejdere som kan lave bookinger.
            {
                new Medarbejder { MedarbejderId = 1, Navn = "Sofie Møller" },
                new Medarbejder { MedarbejderId = 2, Navn = "Amir Rahimi" },
                new Medarbejder { MedarbejderId = 3, Navn = "Louise Falk" },
                new Medarbejder { MedarbejderId = 4, Navn = "Mette Ates" },
                new Medarbejder { MedarbejderId = 5, Navn = "Henrik Krøll" },
                new Medarbejder { MedarbejderId = 6, Navn = "Jonas" }
            };

            List<Moedelokale> lokaler = new List<Moedelokale>()
            // Opretter en liste med de mødelokaler der kan bookes.
            {
                new Moedelokale { LokaleId = 1, Navn = "Mødelokale 1", Kapacitet = 6 },
                new Moedelokale { LokaleId = 2, Navn = "Mødelokale 2", Kapacitet = 8 },
                new Moedelokale { LokaleId = 3, Navn = "Mødelokale 3", Kapacitet = 12 }
            };

            bool kører = true;
            // Variabel der bestemmer om programmet fortsætter med at vise menuen.

            while (kører)
            // While-loopet gør at menuen kører igen og igen indtil brugeren vælger at afslutte.
            {
                Console.WriteLine("=== KontorNord Booking System ===");
                Console.WriteLine("1. Opret booking");
                Console.WriteLine("2. Se bookinger");
                Console.WriteLine("3. Rediger booking");
                Console.WriteLine("4. Slet booking");
                Console.WriteLine("5. Afslut");
                Console.Write("Vælg et nummer: ");

                string valg = Console.ReadLine() ?? "";
                // Læser brugerens valg fra tastaturet.

                Console.WriteLine();

                if (valg == "1")
                // Hvis brugeren vælger 1, oprettes en ny booking.
                {
                    Console.Write("Indtast dato (dd-mm-åååå): ");
                    string dato = Console.ReadLine() ?? "";

                    while (!ErGyldigDato(dato))
                    // Tjekker om datoen er skrevet i korrekt format.
                    {
                        Console.WriteLine("Forkert datoformat.");
                        Console.Write("Indtast dato (dd-mm-åååå): ");
                        dato = Console.ReadLine() ?? "";
                    }

                    Console.Write("Indtast starttid (HH:mm): ");
                    string startTid = Console.ReadLine() ?? "";

                    while (!ErGyldigTid(startTid))
                    // Tjekker om starttid er i korrekt format.
                    {
                        Console.WriteLine("Forkert tidsformat.");
                        Console.Write("Indtast starttid (HH:mm): ");
                        startTid = Console.ReadLine() ?? "";
                    }

                    Console.Write("Indtast sluttid (HH:mm): ");
                    string slutTid = Console.ReadLine() ?? "";

                    while (!ErGyldigTid(slutTid))
                    // Tjekker om sluttid er korrekt.
                    {
                        Console.WriteLine("Forkert tidsformat.");
                        Console.Write("Indtast sluttid (HH:mm): ");
                        slutTid = Console.ReadLine() ?? "";
                    }

                    Console.Write("Indtast note/formål: ");
                    string note = Console.ReadLine() ?? "";

                    Console.WriteLine("Vælg medarbejder:");
                    foreach (Medarbejder medarbejder in medarbejdere)
                    {
                        Console.WriteLine(medarbejder.MedarbejderId + ". " + medarbejder.Navn);
                    }

                    int medarbejderValg;
                    Console.Write("Indtast medarbejdernummer: ");

                    while (!int.TryParse(Console.ReadLine(), out medarbejderValg) ||
                           medarbejderValg < 1 || medarbejderValg > medarbejdere.Count)
                    {
                        Console.WriteLine("Ugyldigt valg.");
                        Console.Write("Indtast medarbejdernummer: ");
                    }

                    Medarbejder valgtMedarbejder = medarbejdere[medarbejderValg - 1];
                    // Finder den medarbejder brugeren valgte.

                    Console.WriteLine("Vælg mødelokale:");
                    foreach (Moedelokale lokale in lokaler)
                    {
                        Console.WriteLine(lokale.LokaleId + ". " + lokale.Navn);
                    }

                    int lokaleValg;
                    Console.Write("Indtast lokalenummer: ");

                    while (!int.TryParse(Console.ReadLine(), out lokaleValg) ||
                           lokaleValg < 1 || lokaleValg > lokaler.Count)
                    {
                        Console.WriteLine("Ugyldigt valg.");
                        Console.Write("Indtast lokalenummer: ");
                    }

                    Moedelokale valgtLokale = lokaler[lokaleValg - 1];
                    // Finder det lokale brugeren valgte.

                    Booking nyBooking = new Booking();
                    // Opretter et nyt booking-objekt.

                    nyBooking.Dato = dato;
                    nyBooking.StartTid = startTid;
                    nyBooking.SlutTid = slutTid;
                    nyBooking.Note = note;
                    nyBooking.Medarbejder = valgtMedarbejder;
                    nyBooking.Lokale = valgtLokale;
                    // Udfylder bookingens oplysninger.

                    Console.WriteLine();
                    Console.WriteLine("Booking der oprettes:");
                    system.VisBookingDetaljer(nyBooking);
                    // Viser en oversigt over bookingen før den gemmes.

                    Console.WriteLine();
                    Console.Write("Vil du oprette bookingen? (ja/nej): ");
                    string svar = Console.ReadLine() ?? "";

                    if (svar.ToLower() == "ja")
                    {
                        system.OpretBooking(nyBooking);
                        Console.WriteLine("Booking oprettet!");
                    }
                    else
                    {
                        Console.WriteLine("Oprettelse blev annulleret.");
                    }
                }

                else if (valg == "5")
                // Hvis brugeren vælger 5, afsluttes programmet.
                {
                    kører = false;
                    Console.WriteLine("Programmet afsluttes...");
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg.");
                }

                Console.WriteLine();
            }
        }

        static bool ErGyldigDato(string dato)
        // Metode der tjekker om en dato er skrevet korrekt.
        {
            return DateTime.TryParseExact(
                dato,
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _);
        }

        static bool ErGyldigTid(string tid)
        // Metode der tjekker om en tid er skrevet korrekt i 24-timers format.
        {
            return DateTime.TryParseExact(
                tid,
                "HH:mm", // HH = timer (00-23) og mm = minutter (00-59)
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _);
        }
    }
}