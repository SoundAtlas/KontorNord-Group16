using KontorNord.Models;

namespace KontorNord.Services
{
	// Class responsible for managing bookings in the system - stores bookings and provides methods to add and retrieve bookings
	public class BookingService
	{
		// List that stores bookings
		private List<Booking> _bookings = new();

		// Adds a new booking 		
		public Booking AddBooking(Booking booking)
		{
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