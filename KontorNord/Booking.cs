using System;
using System.Collections.Generic;
using System.Text;

namespace KontorNord
{
    internal class Booking
    {
        public int Id { get; set; }
        public string RoomName { get; set; } = "";
            public string BookedBy { get; set; } = "";
        public string Date { get; set; } = "";

        public TimeSpan StartTime {  get; set; }
        public TimeSpan EndTime { get; set; }

        public Booking(int id, string roomName , string bookedBy, string date, TimeSpan startTime, TimeSpan endTime)
        {

            Id = id;
            RoomName = roomName;
            BookedBy = bookedBy;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;

        }
    }
}
