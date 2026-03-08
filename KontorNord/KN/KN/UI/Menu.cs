using KN.Models;
using KN.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        public static Medarbejder MedarbejderSelection(BookingSystem system)
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

        public static Moedelokale MoedelokaleSelection(BookingSystem system)
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

        public static void StartNewBooking(BookingSystem system)
        {
            while (true)
            {

                Medarbejder valgtMedarbejder = Menu.MedarbejderSelection(system);

                Console.Clear();
                Console.WriteLine($"{valgtMedarbejder.navn}");

                Console.Clear();

                string[] medarbejderConfirmation =
                {
                        "JA",
                        "NEJ",
                    };


                int choiceconfirmMedarbejder = ConsoleHelpers.ChooseFromList($"MEDARBEJDER: {valgtMedarbejder.navn}\n\nBEKRAEFT?", medarbejderConfirmation);

                if (choiceconfirmMedarbejder == 0)
                {
                    while (true)
                    {
                        Moedelokale valgtMoedelokale = Menu.MoedelokaleSelection(system);

                        Console.Clear();
                        Console.WriteLine($"{valgtMoedelokale.navn}");

                        Console.Clear();

                        string[] moedelokaleConfirmation =
                        {
                               "JA",
                               "NEJ",
                            };

                        int choiceconfirmMoedelokale = ConsoleHelpers.ChooseFromList($"MOEDELOKALE: {valgtMoedelokale.navn}\n\nBEKRAEFT?", moedelokaleConfirmation);

                        if (choiceconfirmMoedelokale == 0)
                        {
                            DateTime valgtDato = ConsoleHelpers.ChooseDate($"VAELG DATO:", DateTime.Today, 2);
                        }
                        else if (choiceconfirmMoedelokale == 1)
                        {
                            continue;
                        }

                        break;
                    }
                }
                else if (choiceconfirmMedarbejder == 1)
                {
                    continue;
                }
            }
        }
    }
}

