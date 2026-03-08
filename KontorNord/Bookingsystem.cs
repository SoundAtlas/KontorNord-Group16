using System; // Giver adgang til grundlæggende funktioner som Console og DateTime.
using System.Collections.Generic; // Giver adgang til List<T>, som bruges til at gemme flere bookinger.
using System.Globalization; // Bruges til at arbejde med datoformat, fx ved validering og konvertering.

namespace KontorNordBooking
// Namespace samler klasserne i projektet, så de hører til samme system.
{
    public class BookingSystem
    // Klassen BookingSystem styrer hele booking-logikken i programmet.
    // Det er her bookinger bliver gemt, vist, fundet, redigeret og slettet.
    {
        public List<Booking> Bookinger = new List<Booking>();
        // Opretter en liste som gemmer alle bookinger i systemet.
        // Hver gang en ny booking oprettes, bliver den lagt ind i denne liste.

        private int næsteBookingId = 1;
        // Denne variabel holder styr på det næste ledige booking-id.
        // Den starter på 1 og stiger hver gang en ny booking bliver oprettet.
        // private betyder, at variablen kun kan bruges inde i denne klasse.

        public void OpretBooking(Booking booking)
        // Denne metode bruges til at oprette en ny booking i systemet.
        {
            booking.BookingId = næsteBookingId;
            // Den nye booking får tildelt det næste ledige ID.

            næsteBookingId++;
            // Efter booking-id er brugt, tælles der 1 op,
            // så næste booking får et nyt unikt ID.

            Bookinger.Add(booking);
            // Tilføjer den nye booking til listen over bookinger.
        }

        public void VisAlleBookinger()
        // Denne metode viser alle bookinger der findes i systemet.
        {
            if (Bookinger.Count == 0)
            // Tjekker om listen er tom.
            {
                Console.WriteLine("Der er ingen bookinger i systemet.");
                // Hvis der ikke findes bookinger, får brugeren en besked.
                return;
                // return stopper metoden her, så resten ikke bliver kørt.
            }

            foreach (Booking booking in Bookinger)
            // Går igennem hver booking i listen en ad gangen.
            {
                VisBookingDetaljer(booking);
                // Kalder en anden metode der viser detaljerne for den aktuelle booking.

                Console.WriteLine("---------------------------");
                // Udskriver en linje mellem hver booking, så det bliver mere overskueligt.
            }
        }

        public void VisBookingerIDag()
        // Denne metode viser kun de bookinger der ligger på dags dato.
        {
            string iDag = DateTime.Today.ToString("dd-MM-yyyy");
            // Finder dagens dato og laver den om til tekst i formatet dd-MM-yyyy.
            // Det gør det muligt at sammenligne med bookingens dato, som også er gemt som tekst.

            bool fundet = false;
            // Denne variabel bruges til at holde styr på,
            // om der blev fundet mindst én booking i dag.

            foreach (Booking booking in Bookinger)
            // Går igennem alle bookinger i listen.
            {
                if (booking.Dato == iDag)
                // Sammenligner bookingens dato med dagens dato.
                {
                    VisBookingDetaljer(booking);
                    // Viser bookingens oplysninger hvis datoen passer.

                    Console.WriteLine("---------------------------");
                    // Udskriver en adskillelseslinje.

                    fundet = true;
                    // Marker at der blev fundet mindst én booking i dag.
                }
            }

            if (!fundet)
            // Hvis fundet stadig er false, betyder det at ingen bookinger matcher dagens dato.
            {
                Console.WriteLine("Der er ingen bookinger i dag.");
                // Viser besked til brugeren.
            }
        }

        public void VisBookingerDenneUge()
        // Denne metode viser alle bookinger som ligger i den aktuelle uge.
        {
            DateTime iDag = DateTime.Today;
            // Henter dagens dato.

            int forskel = (7 + (iDag.DayOfWeek - DayOfWeek.Monday)) % 7;
            // Beregner hvor mange dage der er fra i dag tilbage til mandag i samme uge.
            // Denne beregning bruges for at finde ugens første dag.

            DateTime mandag = iDag.AddDays(-forskel).Date;
            // Trækker det beregnede antal dage fra i dag,
            // så vi får datoen for mandag i denne uge.

            DateTime søndag = mandag.AddDays(6).Date;
            // Lægger 6 dage til mandag for at finde søndag i samme uge.

            bool fundet = false;
            // Holder styr på om der blev fundet bookinger i denne uge.

            foreach (Booking booking in Bookinger)
            // Går igennem alle bookinger i systemet.
            {
                DateTime bookingDato;
                // Variabel der skal bruges til at gemme bookingens dato som DateTime.

                bool gyldigDato = DateTime.TryParseExact(
                    booking.Dato,
                    "dd-MM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out bookingDato);
                // Forsøger at konvertere bookingens dato fra tekst til DateTime.
                // TryParseExact returnerer true hvis datoen har korrekt format.
                // out bookingDato gemmer den konverterede dato hvis det lykkes.

                if (gyldigDato && bookingDato.Date >= mandag && bookingDato.Date <= søndag)
                // Tjekker først at datoen er gyldig.
                // Derefter tjekkes om bookingdatoen ligger mellem mandag og søndag.
                {
                    VisBookingDetaljer(booking);
                    // Viser bookingens detaljer hvis den ligger i denne uge.

                    Console.WriteLine("---------------------------");
                    // Udskriver adskillelseslinje.

                    fundet = true;
                    // Marker at mindst én booking blev fundet.
                }
            }

            if (!fundet)
            // Hvis ingen bookinger blev fundet i denne uge.
            {
                Console.WriteLine("Der er ingen bookinger i denne uge.");
                // Viser besked til brugeren.
            }
        }

        public Booking? FindBooking(int bookingId)
        // Denne metode søger efter en booking ud fra booking-id.
        // ? betyder at metoden godt må returnere null, hvis intet findes.
        {
            foreach (Booking booking in Bookinger)
            // Går igennem alle bookinger.
            {
                if (booking.BookingId == bookingId)
                // Tjekker om bookingens ID matcher det ID brugeren søger efter.
                {
                    return booking;
                    // Returnerer bookingen med det samme hvis den findes.
                }
            }

            return null;
            // Hvis ingen booking matcher ID'et, returneres null.
        }

        public void VisBookingDetaljer(Booking booking)
        // Denne metode viser alle oplysninger om én enkelt booking.
        {
            Console.WriteLine("Booking ID: " + booking.BookingId);
            // Viser bookingens unikke ID.

            Console.WriteLine("Medarbejder: " + booking.Medarbejder.Navn);
            // Viser navnet på den medarbejder der har lavet bookingen.

            Console.WriteLine("Lokale: " + booking.Lokale.Navn);
            // Viser navnet på det mødelokale der er booket.

            Console.WriteLine("Dato: " + booking.Dato);
            // Viser datoen for bookingen.

            Console.WriteLine("Tid: " + booking.StartTid + " - " + booking.SlutTid);
            // Viser bookingens starttid og sluttid.

            Console.WriteLine("Note/Formål: " + booking.Note);
            // Viser noten eller formålet med bookingen.
        }

        public void SletBooking(int bookingId)
        // Denne metode sletter en booking ud fra booking-id.
        {
            Booking? booking = FindBooking(bookingId);
            // Søger først efter bookingen ved hjælp af FindBooking-metoden.

            if (booking != null)
            // Hvis bookingen blev fundet.
            {
                Bookinger.Remove(booking);
                // Fjerner bookingen fra listen.

                Console.WriteLine("Bookingen blev slettet.");
                // Giver brugeren besked om at sletningen lykkedes.
            }
            else
            {
                Console.WriteLine("Booking blev ikke fundet.");
                // Viser besked hvis booking-id ikke findes.
            }
        }

        public void RedigerBooking(int bookingId, string nyDato, string nyStartTid, string nySlutTid, Moedelokale nytLokale, string nyNote)
        // Denne metode bruges til at opdatere en eksisterende booking med nye oplysninger.
        {
            Booking? booking = FindBooking(bookingId);
            // Finder først den booking der skal redigeres.

            if (booking != null)
            // Hvis bookingen findes.
            {
                booking.Dato = nyDato;
                // Opdaterer datoen.

                booking.StartTid = nyStartTid;
                // Opdaterer starttiden.

                booking.SlutTid = nySlutTid;
                // Opdaterer sluttiden.

                booking.Lokale = nytLokale;
                // Opdaterer hvilket lokale der er valgt.

                booking.Note = nyNote;
                // Opdaterer noten eller formålet.

                Console.WriteLine("Bookingen blev opdateret.");
                // Bekræfter overfor brugeren at redigeringen lykkedes.
            }
            else
            {
                Console.WriteLine("Booking blev ikke fundet.");
                // Hvis booking-id ikke findes, vises fejlbesked.
            }
        }
    }
}