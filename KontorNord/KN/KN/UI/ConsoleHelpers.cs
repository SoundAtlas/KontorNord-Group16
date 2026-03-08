using System;
using System.Collections.Generic;
using System.Text;

namespace KN.UI
{
    internal class ConsoleHelpers
    {
        public static void FlushKeyBuffer(string[] titleScreen)
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        public static void RenderCenteredBlock(string[] lines)
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
        
        public static int ChooseFromList(string title, string[] options)
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

                if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    selected--;

                    if (selected < 0)
                    {
                        selected = lastIndex;
                    }


                }

                if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
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

        public static void ShowTitleScreen(int milliseconds)
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

            ConsoleHelpers.RenderCenteredBlock(titleScreen);
            Console.CursorVisible = false;
            Thread.Sleep(milliseconds);
            Console.Clear();
            ConsoleHelpers.FlushKeyBuffer(titleScreen);

        }

        public static DateTime ChooseDate(string title, DateTime initialDate, int yearSpan)
        {
            int day = initialDate.Day;
            int month = initialDate.Month;
            int year = initialDate.Year;

            int activeField = 0;

            int yearMin = DateTime.Today.Year;
            int yearMax = yearMin + yearSpan;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("VAELG DATO\n\n");

                if (activeField == 0)
                {
                    Console.WriteLine($"[{day}] / {month} / {year}");
                }
                if (activeField == 1)
                {
                    Console.WriteLine($"{day} / [{month}] / {year}");
                }
                if (activeField == 2)
                {
                    Console.WriteLine($"{day} / {month} / [{year}]");
                }

                var key = Console.ReadKey(true).Key;
                
                int lastFieldIndex = 2;
                int lastMonth = 12;
                int lastDay = DateTime.DaysInMonth(year, month);
                int lastYear = yearMax;

                if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
                {
                    activeField--;

                    if (activeField < 0)
                    {
                        activeField = lastFieldIndex;
                    }


                }

                if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                {
                    activeField++;

                    if (activeField > lastFieldIndex)
                    {
                        activeField = 0;
                    }
                }

                if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    month++;

                    if (month > 12)
                    {
                        month = 1;
                    }
                }

                if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    month--;

                    if (month < 1)
                    {
                        month = 12;
                    }
                }
            }

        }
    }
}
