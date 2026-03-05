using KontorNord.Models;

namespace KontorNord.Services
{
    // Class responsible for managing bookings in the system - stores bookings and provides methods to add and retrieve bookings
    public class BookingService
    {

        private List<Booking> _bookings = new(); // list that stores bookings
        private int _nextId = 1; // Counter for booking IDs

        // Adds a new booking 		
        public Booking AddBooking(Booking booking)
        {
            booking.Id = _nextId++; // Assign unique ID
            _bookings.Add(booking);
            return booking;
        }

        // Returns all bookings currently stored
        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }

    }
}