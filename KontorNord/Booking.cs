namespace KontorNordBooking
// Namespace bruges til at organisere klasser i projektet.
// Alle klasser i bookingsystemet ligger i samme namespace.

{
    public class Booking
    // Klassen Booking repræsenterer en booking i systemet.
    // En booking indeholder information om dato, tid, medarbejder og mødelokale.
    {
        public int BookingId { get; set; }
        // Unikt ID for hver booking.
        // Bruges til at identificere bookingen når man vil redigere eller slette den.

        public string Dato { get; set; } = "";
        // Gemmer datoen for mødet.
        // Datoen gemmes som tekst i formatet dd-MM-yyyy.

        public string StartTid { get; set; } = "";
        // Gemmer starttidspunktet for mødet.
        // Formatet er HH:mm (24-timers format).

        public string SlutTid { get; set; } = "";
        // Gemmer sluttidspunktet for mødet.
        // Også i formatet HH:mm.

        public string Note { get; set; } = "";
        // En kort beskrivelse af mødet eller formålet med bookingen.
        // For eksempel: "Projektmøde" eller "Kundemøde".

        public Medarbejder Medarbejder { get; set; } = null!;
        // Denne egenskab refererer til den medarbejder der har oprettet bookingen.
        // Klassen Medarbejder indeholder oplysninger som medarbejderens navn og ID.
        // null! bruges for at fortælle compileren at værdien bliver sat senere.

        public Moedelokale Lokale { get; set; } = null!;
        // Denne egenskab refererer til det mødelokale der er booket.
        // Klassen Moedelokale indeholder information om lokalet, fx navn og kapacitet.
        // null! bruges igen fordi værdien først sættes når bookingen oprettes.
    }
}