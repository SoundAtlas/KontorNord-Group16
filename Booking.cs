using System;

namespace SimpleBookingSystem
{
    // En booking indeholder information om et møde
    public class Booking
    {
        public int Id;            // Unikt ID for bookingen
        public MeetingRoom Room;  // Hvilket lokale der er booket
        public DateTime Start;    // Starttidspunkt
        public DateTime End;      // Sluttidspunkt
        public string Note;       // Eventuel note
    }
}