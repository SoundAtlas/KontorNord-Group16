namespace KontorNordBooking
// Namespace bruges til at organisere koden i projektet.
// Alle klasser i dette projekt ligger i samme namespace, så de kan bruge hinanden.

{
    public class Moedelokale
    // Definerer en klasse der repræsenterer et mødelokale i bookingsystemet.
    // Klassen bruges til at gemme oplysninger om et lokale, som kan bookes.

    {
        public int LokaleId { get; set; }
        // En egenskab der gemmer et unikt ID for mødelokalet.
        // ID'et bruges til at identificere lokalet i systemet.
        // { get; set; } betyder at værdien både kan læses og ændres.

        public string Navn { get; set; } = "";
        // En egenskab der gemmer navnet på mødelokalet.
        // Eksempel: "Mødelokale 1".
        // = "" betyder at variablen starter som en tom tekst, så den ikke bliver null.

        public int Kapacitet { get; set; }
        // En egenskab der gemmer hvor mange personer der kan være i lokalet.
        // For eksempel 6, 8 eller 12 personer.
    }
}