using System;
using System.Collections.Generic;

namespace SimpleBookingSystem
{
    // Denne klasse styrer alle bookinger og lokaler
    public class BookingManager
    {
        public List<Booking> bookings = new List<Booking>(); // Liste med alle bookinger
        public List<MeetingRoom> rooms = new List<MeetingRoom>(); // Liste med lokaler
        private int nextId = 1; // Bruges til at give nye bookinger et ID

        public BookingManager()
        {
            // Vi opretter tre mødelokaler
            rooms.Add(new MeetingRoom { Id = 1, Name = "Rød stue" });
            rooms.Add(new MeetingRoom { Id = 2, Name = "Blå stue" });
            rooms.Add(new MeetingRoom { Id = 3, Name = "Grøn stue" });
        }

        // Tjekker om lokalet er ledigt i det ønskede tidsrum
        public bool IsRoomFree(MeetingRoom room, DateTime start, DateTime end)
        {
            foreach (var b in bookings)
            {
                // Hvis det er samme lokale
                if (b.Room.Id == room.Id)
                {
                    // Tjek om tiderne overlapper
                    if (start < b.End && end > b.Start)
                    {
                        return false; // Lokalet er optaget
                    }
                }
            }
            return true; // Lokalet er ledigt
        }

        // Opretter en ny booking
        public Booking CreateBooking(MeetingRoom room, DateTime start, DateTime end, string note)
        {
            Booking b = new Booking();
            b.Id = nextId++; // Giv nyt ID
            b.Room = room;
            b.Start = start;
            b.End = end;
            b.Note = note;

            bookings.Add(b); // Tilføj til listen
            return b;
        }

        // Finder en booking ud fra ID
        public Booking FindBooking(int id)
        {
            foreach (var b in bookings)
            {
                if (b.Id == id)
                {
                    return b;
                }
            }
            return null; // Hvis den ikke findes
        }

        // Sletter en booking
        public void DeleteBooking(Booking b)
        {
            bookings.Remove(b);
        }

        // Finder et lokale ud fra ID
        public MeetingRoom FindRoom(int id)
        {
            foreach (var r in rooms)
            {
                if (r.Id == id)
                {
                    return r;
                }
            }
            return null;
        }
    }
}