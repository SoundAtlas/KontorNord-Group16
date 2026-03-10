using KN.Models;
using KN.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KN.UI
{
    internal class Menu
    {
        public static void MenuMain(BookingSystem system)
        {

            string[] menuMain =
            {
                    "NY BOOKING",
                    "SE BOOKINGER",
                    "SLET BOOKING",
                    "AFSLUT",
                    };

            while (true)
            {
                int? choice = ConsoleHelpers.ChooseFromListOrCancel("HOVEDMENU", menuMain);

                if (choice == null)
                {
                    return;
                }

                if (choice == 0)
                {
                    StartNewBooking(system);
                }

                else if (choice == 1)
                {
                    SeeBookings(system);
                }

                else if (choice == 2)
                {
                    SletBooking(system);
                }

                else if (choice == 3)
                {
                    break;
                }
            }

        }

        public static Medarbejder? MedarbejderSelection(BookingSystem system)
        {
            List<Medarbejder> medarbejdere = system.GetMedarbejdere();
            string[] options = new string[medarbejdere.Count];

            for (int i = 0; i < medarbejdere.Count; i++)
            {
                options[i] = $"{medarbejdere[i].medarbejderId}. {medarbejdere[i].navn}";
            }

            int? selectedIndex = ConsoleHelpers.ChooseFromListOrCancel("VAELG MEDARBEJDER:", options);
            if (selectedIndex == null)
            {
                return null;
            }

            int idx = selectedIndex.Value;
            return medarbejdere[idx];

        }

        public static Moedelokale? MoedelokaleSelection(BookingSystem system)
        {
            List<Moedelokale> moedelokaler = system.GetMoedelokaler();
            string[] options = new string[moedelokaler.Count];

            for (int i = 0; i < moedelokaler.Count; i++)
            {
                options[i] = $"{moedelokaler[i].moedelokaleId}. {moedelokaler[i].navn}";
            }

            int? selectedIndex = ConsoleHelpers.ChooseFromListOrCancel("VAELG MOEDELOKALE:", options);
            if (selectedIndex == null)
            {
                return null;
            }

            int idx = selectedIndex.Value;
            return moedelokaler[idx];
        }

        public static void StartNewBooking(BookingSystem system)
        {
            Medarbejder? valgtMedarbejder = null;
            while (true)
            {

                valgtMedarbejder = MedarbejderSelection(system);

                if (valgtMedarbejder == null) return;
                break;
            }

            (Moedelokale moedelokale, DateTime dato)? lokaleDato = null;
            while (true)
            {
                lokaleDato = ConsoleHelpers.PickRoomAndDate("VAELG LOKALE & DATO:", DateTime.Today, 2, system);
                if (lokaleDato == null) return;
                break;
            }

            Moedelokale? valgtMoedelokale = lokaleDato.Value.moedelokale;
            DateTime valgtDato = lokaleDato.Value.dato;

            List<Booking> bookingsValgtLokaleDato = system.GetBookingMatchesMoedelokaleDato(valgtMoedelokale.moedelokaleId, valgtDato);

            var tidValg = ConsoleHelpers.PickStartTidSlutTid(bookingsValgtLokaleDato, new TimeSpan(8,0,0), new TimeSpan(18,0,0));
            if (tidValg == null)
            {
                return;
            }
            
            (TimeSpan startTid, TimeSpan slutTid) = tidValg.Value;
            
            Console.Clear();

            string[] bookingConfirmation =
            {
                        "JA",
                        "NEJ",
            };

            int? choiceConfirmBooking = ConsoleHelpers.ChooseFromListOrCancel($"BOOKING:\n{valgtMoedelokale.navn}\n{valgtDato:dd/MM/yyyy}\n{startTid:hh\\:mm} - {slutTid:hh\\:mm}\n\nBEKRAEFT?", bookingConfirmation);
            if (choiceConfirmBooking == null) return;

            if (choiceConfirmBooking == 0)
            {
                Booking booking = new Booking();
                booking.moedelokale = valgtMoedelokale;
                booking.medarbejder = valgtMedarbejder;
                booking.dato = valgtDato;
                booking.startTid = startTid;
                booking.slutTid = slutTid;
                system.AddBooking(booking);

                Console.Clear();
                Console.WriteLine("BOOKING OPRETTET");
                Console.ReadKey(true);

                return; 
            }
            if (choiceConfirmBooking == 1)
            {
                return;
            }
        }

        public static void SeeBookings(BookingSystem system)
        {
            Moedelokale? valgtMoedelokale = MoedelokaleSelection(system);
            if (valgtMoedelokale == null) return;

            string[] options =
            {
                "I DAG",
                "DENNE UGE",
                "ALLE",
            };

            int? filterChoice = ConsoleHelpers.ChooseFromListOrCancel("FILTRER:", options);
            if (filterChoice == null) return;

            DateTime today = DateTime.Today;

            DateTime rangeStart = today;   
            DateTime rangeEnd = today;

            if (filterChoice == 0)
            {
                rangeStart = today.Date;
                rangeEnd = today.Date;
            }
            else if (filterChoice == 1)
            {
                int day = (int)today.DayOfWeek;
                if (day == 0) day = 7;
                int daysSinceMonday = day - 1;

                rangeStart = today.AddDays(-daysSinceMonday).Date;
                rangeEnd = rangeStart.AddDays(6).Date;
            }
            
            List<Booking> bookings = system.GetBookings();
            List<Booking> matches = new List<Booking>();
            
            foreach (Booking booking in bookings)
            {
                bool lokaleMatch = booking.moedelokale.moedelokaleId == valgtMoedelokale.moedelokaleId;

                bool datoMatch =
                    filterChoice == 2
                    || (booking.dato.Date >= rangeStart && booking.dato.Date <= rangeEnd);

                if (lokaleMatch && datoMatch)
                {
                    matches.Add(booking);
                }
            }
                        
           
            Console.Clear();
            Console.WriteLine($"{valgtMoedelokale.navn}\n");
            
            if (matches.Count == 0)
            {
                Console.WriteLine("INGEN BOOKINGER...");
            }
            else
            {
                Console.WriteLine("BOOKINGER:");
                foreach (Booking booking in matches)
                {
                    Console.WriteLine($"\n{booking.dato:dd/MM/yyyy}\n{booking.startTid:hh\\:mm} - {booking.slutTid:hh\\:mm}\n{booking.medarbejder.navn}");
                }
            }
            Console.ReadKey(true);
        }

        public static void SletBooking(BookingSystem system)
        {
            Medarbejder? valgtMedarbejder = null;

            while (true)
            {
                valgtMedarbejder = MedarbejderSelection(system);
                if (valgtMedarbejder == null) return;
                break;
            }

            while (true)
            { 
               List<Booking> matches = system.GetBookingMatchesForMedarbejder(valgtMedarbejder.medarbejderId);
               string[] options = new string[matches.Count];

                for (int i = 0; i < matches.Count; i++)
                {
                    options[i] =

                        $"{matches[i].medarbejder.navn}\n{matches[i].moedelokale.navn}\n{matches[i].dato:dd/MM/yyyy}\n{matches[i].startTid:hh\\:mm} - {matches[i].slutTid:hh\\:mm}";
                }

                Console.Clear();

                if (matches.Count == 0)
                {
                    Console.WriteLine("INGEN BOOKINGER...");
                    Console.ReadKey(true);
                    return;
                }
                else
                {
                    int? chosenIndex = ConsoleHelpers.ChooseFromListOrCancel("VAELG BOOKING", options);

                    if (chosenIndex == null)
                    {
                        return;
                    }

                    int idx = chosenIndex.Value;

                    Booking chosenBooking = matches[idx];

                    Console.Clear();

                    string[] chosenIndexConfirmation =
                    {
                               "JA",
                               "NEJ",
                    };

                    int? choiceChosenIndexConfirmation = ConsoleHelpers.ChooseFromListOrCancel($"SLET BOOKING?\n\n{options[idx]}", chosenIndexConfirmation);

                    if (choiceChosenIndexConfirmation == null)
                    {
                        return;
                    }
                    if (choiceChosenIndexConfirmation == 0)
                    {
                        system.DeleteBooking(chosenBooking);

                        Console.Clear();
                        Console.WriteLine("BOOKING SLETTET");
                        Console.ReadKey(true);
                    }
                    else return;
                }
            }
        }

        public static void RedigerBooking(BookingSystem system)
        {
            Medarbejder? valgtMedarbejder = null;

            while (true)
            {
                valgtMedarbejder = MedarbejderSelection(system);
                if (valgtMedarbejder == null) return;
                break;
            }
        }
    }
}
