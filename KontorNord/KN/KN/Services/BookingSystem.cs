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
    
        public List<Moedelokale> GetMoedelokaler()
        {
            return moedelokaler;
        }
    }
}