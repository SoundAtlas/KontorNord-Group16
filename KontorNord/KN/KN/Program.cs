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

        }

        static void MenuMain(BookingSystem system)
        {
            string[] menuMain =
            {
                "1. Ny Booking",
                "2. Se Bookinger",
                "3. Ret Booking",
                "4. Annuller Booking",
                "5. Afslut",
            };
            RenderCenteredBlock(menuMain);
            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.KeyChar)
            {
                
                case '1':

                    Console.Clear();
                    List<Moedelokale> moedelokaler = system.GetMoedelokaler();
                
                    foreach (Moedelokale moedelokale in moedelokaler)
                    {
                        Console.WriteLine($"{moedelokale.moedelokaleId}. {moedelokale.navn}\n");
                    }
                    Console.ReadKey(); 
                    return;

                case '2':
                    return;

                case '3':
                    return;

                case '4':
                    return;

                case '5':
                    return;
            }
        }
    }
}
