using KN.Models;
using KN.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Text;

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
                    "AFSLUT",
                    };

            while (true)
            {
                int choice = ConsoleHelpers.ChooseFromList("HOVEDMENU", menuMain);

                if (choice == 0)
                {
                    StartNewBooking(system);
                }

                else if (choice == 1)
                {
                    Moedelokale valgtMoedelokale = Menu.MoedelokaleSelection(system);

                    Console.Clear();
                    Console.WriteLine($"{valgtMoedelokale.navn}");
                    Console.ReadKey();
                }

                else if (choice == 2)
                {
                    break;
                }

            }

        }
        public static Medarbejder MedarbejderSelectionList(BookingSystem system)
        {
            List<Medarbejder> medarbejdere = system.GetMedarbejdere();
            string[] options = new string[medarbejdere.Count];

            for (int i = 0; i < medarbejdere.Count; i++)
            {
                options[i] = $"{medarbejdere[i].medarbejderId}. {medarbejdere[i].navn}";
            }

            int selectedIndex = ConsoleHelpers.ChooseFromList("VAELG MEDARBEJDER:", options);

            return medarbejdere[selectedIndex];

        }

        public static Medarbejder MedarbejderSelection(BookingSystem system)
        {
            Medarbejder valgtMedarbejder = Menu.MedarbejderSelectionList(system);

            Console.Clear();
            Console.WriteLine($"{valgtMedarbejder.navn}");

            Console.Clear();

            string[] medarbejderConfirmation =
            {
                        "JA",
                        "NEJ",
                    };


            int choiceConfirmMedarbejder = ConsoleHelpers.ChooseFromList($"MEDARBEJDER: {valgtMedarbejder.navn}\n\nBEKRAEFT?", medarbejderConfirmation);

            if (choiceConfirmMedarbejder == 0)
            {
                return valgtMedarbejder;
            }

            else
            {
                return null;
            }

        }

        public static Moedelokale MoedelokaleSelectionList(BookingSystem system)
        {
            List<Moedelokale> moedelokaler = system.GetMoedelokaler();
            string[] options = new string[moedelokaler.Count];

            for (int i = 0; i < moedelokaler.Count; i++)
            {
                options[i] = $"{moedelokaler[i].moedelokaleId}. {moedelokaler[i].navn}";
            }

            int selectedIndex = ConsoleHelpers.ChooseFromList("VAELG MOEDELOKALE:", options);
            return moedelokaler[selectedIndex];
        }

        public static Moedelokale MoedelokaleSelection(BookingSystem system)
        {
            Moedelokale valgtMoedelokale = Menu.MoedelokaleSelectionList(system);

            Console.Clear();
            Console.WriteLine($"{valgtMoedelokale.navn}");

            Console.Clear();

            string[] moedelokaleConfirmation =
            {
                               "JA",
                               "NEJ",
                            };

            int choiceConfirmMoedelokale = ConsoleHelpers.ChooseFromList($"MOEDELOKALE: {valgtMoedelokale.navn}\n\nBEKRAEFT?", moedelokaleConfirmation);

            if (choiceConfirmMoedelokale == 0)
            {
                return valgtMoedelokale;
            }
            else
            {
                return null;
            }
        }

        public static void StartNewBooking(BookingSystem system)
        {

            Medarbejder valgtMedarbejder = null;
            while (true)
            {
                valgtMedarbejder = MedarbejderSelection(system);

                if (valgtMedarbejder == null) continue;
                break;
            }

            (Moedelokale moedelokale, DateTime dato)? lokaleDato = null;
            while (true)
            {
                lokaleDato = ConsoleHelpers.PickRoomAndDate("VAELG LOKALE & DATO:", DateTime.Today, 2, system);
                if (lokaleDato == null) continue;
                break;
            }

            Moedelokale valgtMoedelokale = lokaleDato.Value.moedelokale;
            DateTime valgtDato = lokaleDato.Value.dato;

            List<Booking> bookingsValgtLokaleDato = system.GetBookingMatchesMoedelokaleDato(valgtMoedelokale.moedelokaleId, valgtDato);

            TimeSpan start = new TimeSpan(8, 0, 0);
            TimeSpan end = new TimeSpan(18, 0 , 0);
            TimeSpan step = new TimeSpan(0, 30, 0);

            for (TimeSpan t = start; t <= end; t = t.Add(step))
            {
                bool blocked = false;

                foreach (Booking b in bookingsValgtLokaleDato)
                {
                    if (b.startTid <= t && t < b.slutTid)
                    {
                        blocked = true; 
                        break; 
                    }
                }

                Console.WriteLine($"{(blocked ? "X" : " ")} {t:hh\\:mm}");
            }

            Console.WriteLine("\nFortsaet...");
            Console.ReadKey(true);
        }
    }
}
