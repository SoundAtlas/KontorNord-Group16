using System;
namespace KontorNord.Models
{

    // Class representing a booking in the system - contains properties for the booking's ID, associated meeting room, start and end times, who booked it, and an optional note
    public class Booking
	{
		public int Id { get; set; }

		public int MeetingRoomId { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		public string BookedBy { get; set; } = string.Empty;

		public string? Note { get; set; }
    }
}