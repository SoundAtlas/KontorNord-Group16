namespace KontorNordBooking
// Namespace bruges til at organisere koden i projektet.
// Alle klasser i bookingsystemet ligger i dette namespace.

{
    public class Medarbejder
    // Klassen Medarbejder repræsenterer en medarbejder i virksomheden.
    // En medarbejder er den person der kan oprette en booking af et mødelokale.

    {
        public int MedarbejderId { get; set; }
        // Denne egenskab gemmer et unikt ID for medarbejderen.
        // ID'et bruges til at identificere medarbejderen i systemet.
        // { get; set; } betyder at værdien både kan læses og ændres.

        public string Navn { get; set; } = "";
        // Denne egenskab gemmer medarbejderens navn.
        // Eksempel: "Sofie Møller".
        // = "" betyder at variablen starter som en tom tekst,
        // så den ikke bliver null.
    }
}