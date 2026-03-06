using KN.Models;
using KN.Services;
using System.Data;

namespace KN
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowTitleScreen(4000);

            BookingSystem system = new BookingSystem();

            MenuMain(system);

        }

        static void RenderCenteredBlock(string[] lines)
        {
            int blockHeight = lines.Length;
            int startY = (Console.WindowHeight - blockHeight) / 2;

            for (int i = 0; i < lines.Length; i++)
            {
                int x = (Console.WindowWidth - lines[i].Length) / 2;
                int y = startY + i;
                Console.SetCursorPosition(x, y);
                Console.WriteLine(lines[i]);
            }
        }

        static void FlushKeyBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        static int ChooseFromList(string title, string[] options)
        {
            int selected = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(title + "\n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.WriteLine($"> {options[i]} <");
                    }
                    else
                    {
                        Console.WriteLine($" {options[i]} ");

                    }
                }

                var key = Console.ReadKey(true).Key;
                int lastIndex = options.Length - 1;

                if (key == ConsoleKey.UpArrow)
                {
                    selected--;

                    if (selected < 0)
                    {
                        selected = lastIndex;
                    }


                }

                if (key == ConsoleKey.DownArrow)
                {
                    selected++;

                    if (selected > lastIndex)
                    {
                        selected = 0;
                    }
                }

                if (key == ConsoleKey.Enter)
                {
                    return selected;
                }

                



            }
        }

        static void ShowTitleScreen(int milliseconds)
        {
            string[] titleScreen =
            {
                @$"╔═══════════════════════════════════════════════════════════════════════════════╗",
                @$"║                                                                               ║",
                @$"║            ██████╗  ██████╗  ██████╗ ██╗  ██╗██╗███╗   ██╗ ██████╗            ║",
                @$"║            ██╔══██╗██╔═══██╗██╔═══██╗██║ ██╔╝██║████╗  ██║██╔════╝            ║",
                @$"║            ██████╔╝██║   ██║██║   ██║█████╔╝ ██║██╔██╗ ██║██║  ███╗           ║",
                @$"║            ██╔══██╗██║   ██║██║   ██║██╔═██╗ ██║██║╚██╗██║██║   ██║           ║",
                @$"║            ██████╔╝╚██████╔╝╚██████╔╝██║  ██╗██║██║ ╚████║╚██████╔╝           ║",
                @$"║            ╚═════╝  ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝            ║",
                @$"║                                                                               ║",
                @$"║     _  __  ___   _   _  _____   ___   ____     _   _   ___   ____   ____      ║",
                @$"║    | |/ / / _ \ | \ | ||_   _| / _ \ |  _ \   | \ | | / _ \ |  _ \ |  _ \     ║",
                @$"║    | ' / | | | ||  \| |  | |  | | | || |_) |  |  \| || | | || |_) || | | |    ║",
                @$"║    | . \ | |_| || |\  |  | |  | |_| ||  _ <   | |\  || |_| ||  _ < | |_| |    ║",
                @$"║    |_|\_\ \___/ |_| \_|  |_|   \___/ |_| \_\  |_| \_| \___/ |_| \_\|____/     ║",
                @$"║                                                                               ║",
                @$"║                                                                               ║",
                @$"╚═══════════════════════════════════════════════════════════════════════════════╝",

                    };

            RenderCenteredBlock(titleScreen);
            Console.CursorVisible = false;
            Thread.Sleep(milliseconds);
            Console.Clear();
            FlushKeyBuffer();

        }

        static Medarbejder MedarbejderSelection(BookingSystem system)
        {
            List<Medarbejder> medarbejdere = system.GetMedarbejdere();
            string[] options = new string[medarbejdere.Count];

            for (int i = 0; i < medarbejdere.Count; i++)
            {
                options[i] = $"{medarbejdere[i].medarbejderId}. {medarbejdere[i].navn}";
            }

            int selectedIndex = ChooseFromList("VAELG MEDARBEJDER:", options);
           
            return medarbejdere[selectedIndex];

        }

        static Moedelokale MoedelokaleSelection(BookingSystem system)
        {
            List<Moedelokale> moedelokaler = system.GetMoedelokaler();
            string[] options = new string[moedelokaler.Count];

            for (int i = 0; i < moedelokaler.Count; i++)
            {
                options[i] = $"{moedelokaler[i].moedelokaleId}. {moedelokaler[i].navn}";
            }

            int selectedIndex = ChooseFromList("VAELG MOEDELOKALE:", options);
            return moedelokaler[selectedIndex];
        }

        static void MenuMain(BookingSystem system)
        {
            
            string[] menuMain =
            {
                    "NY BOOKING",
                    "SE BOOKINGER",
                    "AFSLUT",
                    };

            while (true)
            {
                int choice = ChooseFromList("HOVEDMENU", menuMain);

                if (choice == 0)
                {
                   while (true) 
                    {
                    
                    Medarbejder valgtMedarbejder = MedarbejderSelection(system);

                    Console.Clear();
                    Console.WriteLine($"{valgtMedarbejder.navn}");
                    
                        Console.Clear();
                    
                    string[] medarbejderConfirmation =
                    {
                        "JA",
                        "NEJ",
                    };
                    
                    
                        int choiceConfirm = ChooseFromList($"MEDARBEJDER: {valgtMedarbejder.navn}\n\nBEKRAEFT?", medarbejderConfirmation);

                        if (choiceConfirm == 0)
                        {
                            Moedelokale valgtMoedelokale = MoedelokaleSelection(system);

                            break;
                        }
                        else if (choiceConfirm == 1)
                        {
                            continue;
                        }
                    }
                }

                else if (choice == 1)
                {
                    Moedelokale valgtMoedelokale = MoedelokaleSelection(system);
                    
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

    }

    
}       
    

