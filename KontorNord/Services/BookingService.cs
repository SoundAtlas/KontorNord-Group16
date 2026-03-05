using KontorNord.Models;
using System.Text.Json;


namespace KontorNord.Services
{
    // Class responsible for managing bookings in the system - stores bookings and provides methods to add and retrieve bookings
    public class BookingService
    {
        private string _filePath = "bookings.json"; // Path to the file where bookings are stored
        private List<Booking> _bookings = new(); // list that stores bookings
        private int _nextId = 1; // Counter for booking IDs

        // Adds a new booking 		
        public bool TryAddBooking(Booking booking, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (booking.End <= booking.Start) // Validate that end time is after start time
            {
                errorMessage = "Slut tid skal være efter start tid.";
                return false;
            }

            if (booking.Start < DateTime.Now) // Validate that start time is in the future
            {
                errorMessage = "Booking skal være i fremtiden.";
                return false;
            }

            if (HasConflict(booking))
            {
                errorMessage = "Konflikt! Lokalet er allerede booket.";
                return false;
            }

            booking.Id = _nextId++; // Assign unique ID
            _bookings.Add(booking);
            return true;
        }

        // Returns all bookings currently stored
        public List<Booking> GetAllBookings()
        {
            return new List<Booking>(_bookings); // Return a copy of the bookings list to prevent external modification
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

        // Deletes a booking by its ID, returns true if successful, false if not found
        public bool DeleteBooking(int id)
        {
            Booking? booking = GetBookingById(id);
            {
                if (booking == null)
                    return false;

                _bookings.Remove(booking);
                return true;
            }


        }

        // Retrieves a booking by its ID, returns null if not found
        public Booking? GetBookingById(int id)
        {
            foreach (var b in _bookings)
            {
                if (b.Id == id)
                    return b; // Return the booking if found
            }

            return null; // Return null if no booking with the specified ID is found
        }

        // Removes expired bookings

        public void RemoveExpiredBookings()
        {
            DateTime now = DateTime.Now;
            for (int i = _bookings.Count - 1; i >= 0; i--) // 
            {
                if (_bookings[i].End < now) // Check if the booking has already ended
                    _bookings.RemoveAt(i); // Remove expired booking
            }
            _bookings.RemoveAll(b => b.End < now); // Remove bookings that have already ended
        }

        public void SaveToFile()
        {
            string json = JsonSerializer.Serialize(_bookings);
            File.WriteAllText(_filePath, json);
        }


        public void LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return; // No file to load, start with an empty list
            }

            string json = File.ReadAllText(_filePath);

            List<Booking>? loadedBookings = JsonSerializer.Deserialize<List<Booking>>(json);

            if (loadedBookings != null)
            {
                _bookings = loadedBookings;
            }

            _nextId = 1;

            foreach (var b in _bookings)
            {
                if (b.Id >= _nextId)
                {
                    _nextId = b.Id + 1;
                }
            }
        }
    }
}