namespace KN.Models
{
    internal class Booking
    {
        public int bookingId;
        public DateTime dato;
        public TimeSpan startTid;
        public TimeSpan slutTid;
        public Medarbejder medarbejder;
        public Moedelokale moedelokale;
    }
}
