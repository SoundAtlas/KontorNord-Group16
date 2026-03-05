using KontorNord.Models;

namespace KontorNord.Services
{
    // Class responsible for managing bookings in the system - stores bookings and provides methods to add and retrieve bookings
    public class BookingService
    {

        private List<Booking> _bookings = new(); // list that stores bookings
        private int _nextId = 1; // Counter for booking IDs

        // Adds a new booking 		
        public bool TryAddBooking(Booking booking)
        {
            if (booking.End <= booking.Start) // Validate that end time is after start time
                return false;

            if (HasConflict(booking))
                return false;

            booking.Id = _nextId++; // Assign unique ID
            _bookings.Add(booking);
            return true;
        }

        // Returns all bookings currently stored
        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }

        // Checks for conflicts with stored bookings
        private bool HasConflict(Booking candidate)
        {
            foreach (var b in _bookings)
            {

                if (b.RoomId != candidate.RoomId)
                    continue; // Different room, no conflict

                // Check for time overlap
                bool overlaps = candidate.Start < b.End && candidate.End > b.Start;

                if (overlaps)
                    return true; // Found a conflict
            }

            return false; // No conflicts found
        }

        public bool DeleteBooking(int id)
        {
            Booking? toDelete = null;

            // Find the booking with the specified ID
            foreach (var b in _bookings)
            {
                if (b.Id == id)
                {
                    toDelete = b;
                    break;
                }
            }

            // If no booking with the specified ID is found, return false
            if (toDelete == null)
                return false;

            // Remove the found booking from the list
            _bookings.Remove(toDelete);
            return true;


        }
    }
}