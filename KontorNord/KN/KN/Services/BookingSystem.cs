using KN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KN.Services
{
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
            InitializeBookings();
        }
        
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
            arcturus.navn = "Arcturus";
            moedelokaler.Add(arcturus);

            Moedelokale betelgeuse = new Moedelokale();
            betelgeuse.moedelokaleId = 2;
            betelgeuse.navn = "Betelgeuse";
            moedelokaler.Add(betelgeuse);

            Moedelokale canopus = new Moedelokale();
            canopus.moedelokaleId = 3;
            canopus.navn = "Canopus";
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
    }
}