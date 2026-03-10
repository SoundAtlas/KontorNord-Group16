using KN.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KN.Services
{
    internal class BookingSaveModel
    {
        public int bookingId { get; set; }
        public int medarbejderId { get; set; }
        public int moedelokaleId { get; set; }
        public DateTime dato { get; set; }
        public int startMinutes { get; set; }
        public int slutMinutes { get; set; }
        public string note { get; set; } = "";
    }
    internal class BookingSystem
    {
        private List<Booking> bookings;
        private List<Medarbejder> medarbejdere;
        private List<Moedelokale> moedelokaler;


        public BookingSystem()
        {
            bookings = new List<Booking>();
            medarbejdere = new List<Medarbejder>();
            moedelokaler = new List<Moedelokale>();

            InitializeMedarbejdere();
            InitializeMoedelokaler();
            LoadBookingsFromFile();
        }

        private const string BookingsFileName = "bookings.json";
        private void InitializeMedarbejdere()
        {
            Medarbejder sofieMoeller = new Medarbejder();
            sofieMoeller.medarbejderId = 1;
            sofieMoeller.navn = "Sofie Moeller";
            sofieMoeller.rolle = "Projekt- og administrationsmedarbejder";
            medarbejdere.Add(sofieMoeller);


            Medarbejder amirRahimi = new Medarbejder();
            amirRahimi.medarbejderId = 2;
            amirRahimi.navn = "Amir Rahimi";
            amirRahimi.rolle = "Ergonomikonsulent og ergoterapeut";
            medarbejdere.Add(amirRahimi);

            Medarbejder jonasTved = new Medarbejder();
            jonasTved.medarbejderId = 3;
            jonasTved.navn = "Jonas Tved";
            jonasTved.rolle = "Lager- og logistikmedarbejder";
            medarbejdere.Add(jonasTved);

            Medarbejder louiseFalk = new Medarbejder();
            louiseFalk.medarbejderId = 4;
            louiseFalk.navn = "Louise Falk";
            louiseFalk.rolle = "Indretningskonsulent og designansvarlig";
            medarbejdere.Add(louiseFalk);

            Medarbejder metteAtes = new Medarbejder();
            metteAtes.medarbejderId = 5;
            metteAtes.navn = "Mette Ates";
            metteAtes.rolle = "Receptionist og administrativ koordinator";
            medarbejdere.Add(metteAtes);

            Medarbejder henrikKroell = new Medarbejder();
            henrikKroell.medarbejderId = 6;
            henrikKroell.navn = "Henrik Kroell";
            henrikKroell.rolle = "Direktoer";
            medarbejdere.Add(henrikKroell);
        }

        private void InitializeMoedelokaler()
        {
            Moedelokale arcturus = new Moedelokale();
            arcturus.moedelokaleId = 1;
            arcturus.navn = "ARCTURUS";
            moedelokaler.Add(arcturus);

            Moedelokale betelgeuse = new Moedelokale();
            betelgeuse.moedelokaleId = 2;
            betelgeuse.navn = "BETELGEUSE";
            moedelokaler.Add(betelgeuse);

            Moedelokale canopus = new Moedelokale();
            canopus.moedelokaleId = 3;
            canopus.navn = "CANOPUS";
            moedelokaler.Add(canopus);
        }

        private void InitializeBookings()
        {
            Booking booking = new Booking();
            booking.moedelokale = moedelokaler[0];
            booking.medarbejder = medarbejdere[1];
            booking.dato = DateTime.Today;
            booking.startTid = new TimeSpan(9, 0, 0);
            booking.slutTid = new TimeSpan(10, 30, 0);
            bookings.Add(booking);
        }

        public void AddBooking(Booking booking)
        {
            bookings.Add(booking);
            SaveBookingsToFile();
        }

        public List<Medarbejder> GetMedarbejdere()
        {
            return medarbejdere;
        }

        public List<Moedelokale> GetMoedelokaler()
        {
            return moedelokaler;
        }

        public List<Booking> GetBookingMatchesMoedelokaleDato(int roomId, DateTime dato)
        {
            List<Booking> matches = new List<Booking>();

            foreach (Booking booking in bookings)
            {
                if ((booking.moedelokale.moedelokaleId == roomId)
                    && (booking.dato.Date == dato.Date))
                {
                    matches.Add(booking);
                }
            }

            return matches;
        }

        private void SaveBookingsToFile()
        {
            var saveList = new List<BookingSaveModel>();

            foreach (var b in bookings)
            {
                saveList.Add(new BookingSaveModel
                {
                    bookingId = b.bookingId,
                    medarbejderId = b.medarbejder.medarbejderId,
                    moedelokaleId = b.moedelokale.moedelokaleId,
                    dato = b.dato.Date,
                    startMinutes = (int)b.startTid.TotalMinutes,
                    slutMinutes = (int)b.slutTid.TotalMinutes,
                    note = b.note ?? ""
                });
            }

            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = System.Text.Json.JsonSerializer.Serialize(saveList, options);
            File.WriteAllText(BookingsFileName, json);
        }

        private void LoadBookingsFromFile()
        {
            if (!File.Exists(BookingsFileName))
                return;

            string json = File.ReadAllText(BookingsFileName);

            List<BookingSaveModel>? saveList =
                System.Text.Json.JsonSerializer.Deserialize<List<BookingSaveModel>>(json);

            if (saveList == null)
                return;
            
            bookings.Clear();

            foreach (var s in saveList)
            {
                Medarbejder? m = medarbejdere.Find(x => x.medarbejderId == s.medarbejderId);
                Moedelokale? r = moedelokaler.Find(x => x.moedelokaleId == s.moedelokaleId);

                if (m == null || r == null)
                    continue;

                Booking b = new Booking();
                b.bookingId = s.bookingId;
                b.medarbejder = m;
                b.moedelokale = r;
                b.dato = s.dato.Date;
                b.startTid = TimeSpan.FromMinutes(s.startMinutes);
                b.slutTid = TimeSpan.FromMinutes(s.slutMinutes);
                b.note = s.note ?? "";

                bookings.Add(b);
            }
        }

        public List<Booking> GetBookings()
        {
            return bookings; 
        }
    }
}