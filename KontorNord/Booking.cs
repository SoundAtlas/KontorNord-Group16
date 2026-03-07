using System;
using System.Collections.Generic;
using System.Text;

namespace KontorNord
{
    internal class Booking
    {
        public string RoomName { get; set; } = "";
            public string BookedBy { get; set; } = "";
        public string Date { get; set; } = "";

        public Booking(string roomName , string bookedBy, string date)
        {

            RoomName = roomName;
            BookedBy = bookedBy;
            Date = date;

        }
    }
}
