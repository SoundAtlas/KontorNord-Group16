using System;
using System.Collections.Generic;
using System.Text;

namespace KN.Models
{
    internal class Booking
    {
        public int bookingId;
        public DateTime dato;
        public TimeSpan startTid;
        public TimeSpan slutTid;
        public string note = "";
        public Medarbejder medarbejder;
        public Moedelokale moedelokale;
    }
}
