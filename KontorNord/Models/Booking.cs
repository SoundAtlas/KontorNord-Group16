using System;
namespace KontorNord.Models
{
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