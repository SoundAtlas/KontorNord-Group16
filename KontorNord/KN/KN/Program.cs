using KN.Models;
using KN.Services;

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

            int selectedIndex = ChooseFromList("Vaelg medarbejder", options);
           
            return medarbejdere[selectedIndex];

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
                    Medarbejder m = MedarbejderSelection(system);

                    Console.Clear();
                    Console.WriteLine($"{m.navn}");
                    Console.ReadKey();
                }

                else if (choice == 1)
                {

                }

                else if (choice == 2)
                {
                    break;
                }

            }

        }

    }

    
}       
    

