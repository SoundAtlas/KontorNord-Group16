using System;
// using betyder, at vi importerer et bibliotek.
// System giver adgang til grundlæggende funktioner i C#.
// Her bruger vi fx Console til at skrive/læse i konsollen og DateTime til dato og tid.

using System.Collections.Generic;
// Giver adgang til generiske samlinger.
// Her bruges det især til List<T>, som er en liste med flere objekter.

using System.Globalization;
// Giver adgang til kultur- og formatindstillinger.
// Her bruges det til at validere dato og tid med bestemte formater.

namespace KontorNordBooking
// namespace betyder navnerum.
// Det bruges til at samle klasser, der hører til samme projekt.
// Det gør koden mere organiseret og undgår navnekonflikter.

{
    internal class Program
    // class betyder klasse.
    // En klasse er en skabelon eller beholder for kode og data.
    // Program er hovedklassen, hvor programmet starter.
    // internal betyder, at klassen kun kan bruges inden for samme projekt.

    {
        static void Main(string[] args)
        // Main er startpunktet for programmet.
        // static betyder, at metoden kan bruges uden at oprette et objekt af klassen Program.
        // void betyder, at metoden ikke returnerer nogen værdi.
        // string[] args er en liste af tekstargumenter, som kan sendes med ved opstart.
        {
            BookingSystem system = new BookingSystem();
            // Her oprettes et objekt af klassen BookingSystem.
            // BookingSystem er den klasse, der håndterer logikken for oprettelse,
            // visning, redigering og sletning af bookinger.
            // system er navnet på variablen, som refererer til objektet.

            List<Medarbejder> medarbejdere = new List<Medarbejder>()
            // Her oprettes en liste med medarbejdere.
            // List<Medarbejder> betyder en liste, som kun må indeholde Medarbejder-objekter.
            // medarbejdere er navnet på variablen.
            {
                new Medarbejder { MedarbejderId = 1, Navn = "Sofie Møller" },
                // new betyder, at vi opretter et nyt objekt.
                // Medarbejder er klassen.
                // { MedarbejderId = 1, Navn = "Sofie Møller" } sætter værdier på objektets egenskaber.

                new Medarbejder { MedarbejderId = 2, Navn = "Amir Rahimi" },
                new Medarbejder { MedarbejderId = 3, Navn = "Louise Falk" },
                new Medarbejder { MedarbejderId = 4, Navn = "Mette Ates" },
                new Medarbejder { MedarbejderId = 5, Navn = "Henrik Krøll" },
                new Medarbejder { MedarbejderId = 6, Navn = "Jonas" }
                // Disse linjer opretter de medarbejdere, som brugeren kan vælge mellem i systemet.
            };

            List<Moedelokale> lokaler = new List<Moedelokale>()
            // Her oprettes en liste med mødelokaler.
            // List<Moedelokale> betyder en liste med objekter af typen Moedelokale.
            {
                new Moedelokale { LokaleId = 1, Navn = "Mødelokale 1 Kapacitet 6", Kapacitet = 6 },
                // Opretter mødelokale 1 med ID 1, navn og kapacitet 6.

                new Moedelokale { LokaleId = 2, Navn = "Mødelokale 2 Kapacitet 8", Kapacitet = 8 },
                // Opretter mødelokale 2 med kapacitet 8.

                new Moedelokale { LokaleId = 3, Navn = "Mødelokale 3 Kapacitet 12", Kapacitet = 12 }
                // Opretter mødelokale 3 med kapacitet 12.
            };

            bool kører = true;
            // bool betyder boolesk værdi, altså enten true eller false.
            // kører bruges som styring af programmets menu.
            // Så længe kører er true, bliver menuen ved med at blive vist.

            while (kører)
            // while betyder, at løkken fortsætter så længe betingelsen er sand.
            // Her betyder det: så længe kører er true, skal programmet fortsætte.
            {
                Console.WriteLine("=== KontorNord Booking System ===");
                // Console.WriteLine skriver tekst i konsollen og går derefter ned på næste linje.

                Console.WriteLine("1. Opret booking");
                Console.WriteLine("2. Se alle bookinger");
                Console.WriteLine("3. Rediger booking");
                Console.WriteLine("4. Slet booking");
                Console.WriteLine("5. Afslut");
                // Disse linjer viser hovedmenuen for brugeren.

                Console.Write("Vælg et nummer: ");
                // Console.Write skriver tekst uden at gå til næste linje.
                // Brugeren skal nu skrive sit valg.

                string valg = Console.ReadLine() ?? "";
                // string betyder tekst.
                // valg gemmer brugerens input.
                // Console.ReadLine() læser det brugeren skriver.
                // ?? "" betyder: hvis værdien er null, bruges en tom tekst i stedet.

                Console.WriteLine();
                // Skriver en tom linje for at gøre udskriften mere overskuelig.

                if (valg == "1")
                // if bruges til at tjekke en betingelse.
                // Hvis brugeren har skrevet "1", skal programmet oprette en booking.
                {
                    Console.Write("Indtast dato (dd-mm-åååå): ");
                    // Beder brugeren om at indtaste en dato.

                    string dato = Console.ReadLine() ?? "";
                    // Gemmer den indtastede dato som tekst.

                    while (!ErGyldigDato(dato))
                    // ! betyder "ikke".
                    // ErGyldigDato(dato) returnerer true hvis datoen er korrekt.
                    // Så længe datoen IKKE er gyldig, skal brugeren prøve igen.
                    {
                        Console.WriteLine("Forkert datoformat.");
                        // Viser fejlbesked.

                        Console.Write("Indtast dato (dd-mm-åååå): ");
                        // Beder om dato igen.

                        dato = Console.ReadLine() ?? "";
                        // Gemmer det nye input.
                    }

                    Console.Write("Indtast starttid (HH:mm): ");
                    // Beder om starttid i 24-timers format.

                    string startTid = Console.ReadLine() ?? "";
                    // Gemmer starttid som tekst.

                    while (!ErGyldigTid(startTid))
                    // Tjekker om starttiden er i korrekt format.
                    {
                        Console.WriteLine("Forkert tidsformat.");
                        Console.Write("Indtast starttid (HH:mm): ");
                        startTid = Console.ReadLine() ?? "";
                    }

                    Console.Write("Indtast sluttid (HH:mm): ");
                    // Beder om sluttid.

                    string slutTid = Console.ReadLine() ?? "";
                    // Gemmer sluttid som tekst.

                    while (!ErGyldigTid(slutTid))
                    // Tjekker om sluttiden er korrekt.
                    {
                        Console.WriteLine("Forkert tidsformat.");
                        Console.Write("Indtast sluttid (HH:mm): ");
                        slutTid = Console.ReadLine() ?? "";
                    }

                    Console.Write("Indtast note/formål: ");
                    // Beder brugeren om en note eller formål med mødet.

                    string note = Console.ReadLine() ?? "";
                    // Gemmer noten som tekst.

                    Console.WriteLine("Vælg medarbejder:");
                    // Viser overskrift til valg af medarbejder.

                    foreach (Medarbejder medarbejder in medarbejdere)
                    // foreach bruges til at gennemgå alle elementer i en liste.
                    // Her går vi igennem alle medarbejdere i listen medarbejdere.
                    {
                        Console.WriteLine(medarbejder.MedarbejderId + ". " + medarbejder.Navn);
                        // Viser medarbejderens ID og navn.
                        // + bruges til at sammensætte tekst.
                    }

                    int medarbejderValg;
                    // int betyder heltal.
                    // Variablen medarbejderValg skal gemme brugerens valg af medarbejdernummer.

                    Console.Write("Indtast medarbejdernummer: ");
                    // Beder brugeren vælge en medarbejder.

                    while (!int.TryParse(Console.ReadLine(), out medarbejderValg) ||
                           medarbejderValg < 1 || medarbejderValg > medarbejdere.Count)
                    // int.TryParse forsøger at lave brugerens input om til et heltal.
                    // out medarbejderValg betyder, at resultatet gemmes i variablen medarbejderValg.
                    // || betyder "eller".
                    // Betingelsen siger:
                    // hvis input ikke er et tal, eller tallet er mindre end 1,
                    // eller større end antallet af medarbejdere, så er valget ugyldigt.
                    {
                        Console.WriteLine("Ugyldigt valg.");
                        Console.Write("Indtast medarbejdernummer: ");
                    }

                    Medarbejder valgtMedarbejder = medarbejdere[medarbejderValg - 1];
                    // Her hentes den valgte medarbejder fra listen.
                    // [ ] bruges til indeks i en liste.
                    // -1 bruges fordi lister starter ved indeks 0,
                    // men brugeren vælger fra 1.

                    Console.WriteLine("Vælg mødelokale:");
                    // Overskrift til valg af lokale.

                    foreach (Moedelokale lokale in lokaler)
                    // Gennemgår alle lokaler i listen lokaler.
                    {
                        Console.WriteLine(lokale.LokaleId + ". " + lokale.Navn);
                        // Viser lokale-id og lokalenavn.
                    }

                    int lokaleValg;
                    // Variabel til at gemme brugerens valg af lokale.

                    Console.Write("Indtast lokalenummer: ");
                    // Beder om lokalenummer.

                    while (!int.TryParse(Console.ReadLine(), out lokaleValg) ||
                           lokaleValg < 1 || lokaleValg > lokaler.Count)
                    // Samme validering som ved medarbejdervalg.
                    {
                        Console.WriteLine("Ugyldigt valg.");
                        Console.Write("Indtast lokalenummer: ");
                    }

                    Moedelokale valgtLokale = lokaler[lokaleValg - 1];
                    // Henter det lokale brugeren valgte.

                    Booking nyBooking = new Booking();
                    // Opretter et nyt objekt af klassen Booking.
                    // Dette objekt skal indeholde alle oplysninger om den nye booking.

                    nyBooking.Dato = dato;
                    // Sætter bookingens dato.

                    nyBooking.StartTid = startTid;
                    // Sætter bookingens starttid.

                    nyBooking.SlutTid = slutTid;
                    // Sætter bookingens sluttid.

                    nyBooking.Note = note;
                    // Sætter bookingens note.

                    nyBooking.Medarbejder = valgtMedarbejder;
                    // Knytter den valgte medarbejder til bookingen.

                    nyBooking.Lokale = valgtLokale;
                    // Knytter det valgte lokale til bookingen.

                    Console.WriteLine();
                    // Skriver en tom linje.

                    Console.WriteLine("Booking der oprettes:");
                    // Viser tekst før bookingdetaljerne.

                    system.VisBookingDetaljer(nyBooking);
                    // Kalder metoden VisBookingDetaljer i objektet system.
                    // Den viser alle oplysninger om den booking, der er lavet.

                    Console.WriteLine();
                    // Tom linje.

                    Console.Write("Vil du oprette bookingen? (ja/nej): ");
                    // Beder brugeren bekræfte.

                    string svar = Console.ReadLine() ?? "";
                    // Gemmer brugerens svar.

                    if (svar.ToLower() == "ja")
                    // ToLower() gør teksten til små bogstaver.
                    // Så bliver "Ja", "JA" og "ja" behandlet ens.
                    {
                        system.OpretBooking(nyBooking);
                        // Kalder metoden der gemmer bookingen i systemet.

                        Console.WriteLine("Booking oprettet!");
                        // Bekræfter at bookingen er oprettet.
                    }
                    else
                    // else betyder: hvis if-betingelsen ikke er sand.
                    {
                        Console.WriteLine("Oprettelse blev annulleret.");
                        // Fortæller at brugeren ikke oprettede bookingen.
                    }
                }
                else if (valg == "2")
                // else if bruges til at teste en ny betingelse, hvis den forrige ikke var sand.
                // Hvis brugeren vælger 2, vises alle bookinger.
                {
                    system.VisAlleBookinger();
                    // Kalder metoden som viser alle bookinger i systemet.
                }
                else if (valg == "3")
                // Hvis brugeren vælger 3, skal en booking redigeres.
                {
                    system.VisAlleBookinger();
                    // Viser først alle bookinger, så brugeren kan se booking-id.

                    Console.WriteLine();
                    // Tom linje.

                    int bookingId;
                    // Variabel til det booking-id brugeren vil redigere.

                    Console.Write("Indtast booking ID: ");
                    // Beder brugeren indtaste ID.

                    while (!int.TryParse(Console.ReadLine(), out bookingId))
                    // Tjekker at input er et gyldigt heltal.
                    {
                        Console.WriteLine("Ugyldigt tal.");
                        Console.Write("Indtast booking ID: ");
                    }

                    Booking? booking = system.FindBooking(bookingId);
                    // Søger efter en booking med det valgte ID.
                    // Booking? betyder, at variablen godt må være null,
                    // hvis der ikke findes en booking med det ID.

                    if (booking != null)
                    // != betyder "ikke lig med".
                    // Hvis booking ikke er null, så blev den fundet.
                    {
                        Console.WriteLine();
                        Console.WriteLine("Booking fundet:");
                        system.VisBookingDetaljer(booking);
                        // Viser detaljerne for den fundne booking.

                        Console.WriteLine();

                        Console.Write("Vil du redigere denne booking? (ja/nej): ");
                        // Brugeren skal bekræfte redigering.

                        string svar = Console.ReadLine() ?? "";
                        // Gemmer svaret.

                        if (svar.ToLower() == "ja")
                        // Hvis brugeren svarer ja, fortsætter redigeringen.
                        {
                            Console.Write("Ny dato (dd-mm-åååå): ");
                            string nyDato = Console.ReadLine() ?? "";
                            // Beder om ny dato.

                            while (!ErGyldigDato(nyDato))
                            // Validerer den nye dato.
                            {
                                Console.WriteLine("Forkert datoformat.");
                                Console.Write("Ny dato (dd-mm-åååå): ");
                                nyDato = Console.ReadLine() ?? "";
                            }

                            Console.Write("Ny starttid (HH:mm): ");
                            string nyStartTid = Console.ReadLine() ?? "";
                            // Beder om ny starttid.

                            while (!ErGyldigTid(nyStartTid))
                            // Validerer den nye starttid.
                            {
                                Console.WriteLine("Forkert tidsformat.");
                                Console.Write("Ny starttid (HH:mm): ");
                                nyStartTid = Console.ReadLine() ?? "";
                            }

                            Console.Write("Ny sluttid (HH:mm): ");
                            string nySlutTid = Console.ReadLine() ?? "";
                            // Beder om ny sluttid.

                            while (!ErGyldigTid(nySlutTid))
                            // Validerer den nye sluttid.
                            {
                                Console.WriteLine("Forkert tidsformat.");
                                Console.Write("Ny sluttid (HH:mm): ");
                                nySlutTid = Console.ReadLine() ?? "";
                            }

                            Console.Write("Ny note: ");
                            string nyNote = Console.ReadLine() ?? "";
                            // Beder om ny note.

                            Console.WriteLine("Vælg nyt lokale:");
                            // Viser valg af nyt lokale.

                            foreach (Moedelokale lokale in lokaler)
                            // Gennemgår alle lokaler.
                            {
                                Console.WriteLine(lokale.LokaleId + ". " + lokale.Navn);
                            }

                            int lokaleValg;
                            // Variabel til nyt lokalevalg.

                            Console.Write("Indtast lokalenummer: ");

                            while (!int.TryParse(Console.ReadLine(), out lokaleValg) ||
                                   lokaleValg < 1 || lokaleValg > lokaler.Count)
                            // Validerer lokalenummer.
                            {
                                Console.WriteLine("Ugyldigt valg.");
                                Console.Write("Indtast lokalenummer: ");
                            }

                            Moedelokale nytLokale = lokaler[lokaleValg - 1];
                            // Henter det nye valgte lokale fra listen.

                            system.RedigerBooking(bookingId, nyDato, nyStartTid, nySlutTid, nytLokale, nyNote);
                            // Kalder metoden der opdaterer den valgte booking med de nye værdier.

                            Console.WriteLine();
                            Console.WriteLine("Opdateret oversigt:");
                            // Viser tekst efter opdatering.

                            system.VisAlleBookinger();
                            // Viser alle bookinger igen, så brugeren kan se ændringen.
                        }
                    }
                    else
                    // Hvis booking er null, blev bookingen ikke fundet.
                    {
                        Console.WriteLine("Booking blev ikke fundet.");
                    }
                }
                else if (valg == "4")
                // Hvis brugeren vælger 4, skal en booking slettes.
                {
                    system.VisAlleBookinger();
                    // Viser alle bookinger først.

                    Console.WriteLine();

                    int bookingId;
                    // Variabel til booking-id der skal slettes.

                    Console.Write("Indtast booking ID: ");

                    while (!int.TryParse(Console.ReadLine(), out bookingId))
                    // Kontrollerer at input er et tal.
                    {
                        Console.WriteLine("Ugyldigt tal.");
                        Console.Write("Indtast booking ID: ");
                    }

                    Booking? booking = system.FindBooking(bookingId);
                    // Finder den booking der svarer til ID'et.

                    if (booking != null)
                    // Hvis bookingen findes.
                    {
                        Console.WriteLine();
                        Console.WriteLine("Booking fundet:");
                        system.VisBookingDetaljer(booking);
                        // Viser den fundne booking.

                        Console.WriteLine();

                        Console.Write("Vil du slette denne booking? (ja/nej): ");
                        // Beder brugeren bekræfte sletning.

                        string svar = Console.ReadLine() ?? "";
                        // Gemmer svaret.

                        if (svar.ToLower() == "ja")
                        // Hvis brugeren svarer ja, slettes bookingen.
                        {
                            system.SletBooking(bookingId);
                            // Kalder metoden der sletter bookingen.

                            Console.WriteLine("Booking slettet.");
                            // Bekræfter at bookingen blev slettet.
                        }
                    }
                    else
                    {
                        Console.WriteLine("Booking blev ikke fundet.");
                        // Viser besked hvis booking-id ikke findes.
                    }
                }
                else if (valg == "5")
                // Hvis brugeren vælger 5, skal programmet afsluttes.
                {
                    kører = false;
                    // Sætter kører til false.
                    // Det stopper while-løkken.

                    Console.WriteLine("Programmet afsluttes...");
                    // Viser besked om at programmet stopper.
                }
                else
                // Hvis brugeren indtaster noget andet end 1, 2, 3, 4 eller 5.
                {
                    Console.WriteLine("Ugyldigt valg.");
                    // Viser fejlbesked.
                }

                Console.WriteLine();
                // Tom linje før menuen vises igen.
            }
        }

        static bool ErGyldigDato(string dato)
        // Dette er en metode som returnerer true eller false.
        // static betyder, at metoden kan bruges uden et objekt af Program.
        // bool betyder, at resultatet er sandt eller falsk.
        // Metoden tjekker om datoen er i korrekt format.
        {
            return DateTime.TryParseExact(
                dato,
                // Den tekst som brugeren har indtastet.

                "dd-MM-yyyy",
                // Det ønskede datoformat:
                // dd = dag med to cifre
                // MM = måned med to cifre
                // yyyy = år med fire cifre

                CultureInfo.InvariantCulture,
                // Bruger et fast kultur-uafhængigt format.

                DateTimeStyles.None,
                // Ingen ekstra specielle regler for datoen.

                out _);
            // out bruges til at returnere en ekstra værdi fra metoden.
            // _ betyder, at vi ikke vil gemme selve dato-objektet,
            // vi vil kun vide om konverteringen lykkedes.
        }

        static bool ErGyldigTid(string tid)
        // Metode der tjekker om en tid er skrevet korrekt.
        {
            return DateTime.TryParseExact(
                tid,
                // Den tekst brugeren har skrevet som tid.

                "HH:mm",
                // Det ønskede tidsformat:
                // HH = timer i 24-timers format
                // mm = minutter

                CultureInfo.InvariantCulture,
                // Fast kultur-uafhængigt format.

                DateTimeStyles.None,
                // Ingen ekstra regler.

                out _);
            // Vi er kun interesseret i om tiden er gyldig, ikke i at gemme den.
        }
    }
}