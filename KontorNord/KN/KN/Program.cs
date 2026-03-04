namespace KN
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowTitleScreen(4000);
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
            string[] TitleScreen =
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

            RenderCenteredBlock(TitleScreen);
            Console.CursorVisible = false;
            Thread.Sleep(milliseconds);
            Console.Clear();

        }

    }
}
