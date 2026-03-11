using KN.Models;
using KN.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using static System.Net.WebRequestMethods;

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
        
        public static int? ChooseFromListOrCancel(string title, string[] options)
        {
            int selected = 0;
            Console.CursorVisible = false;
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(title + "\n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.WriteLine($" > {options[i]}  ");
                    }
                    else
                    {
                        Console.WriteLine($"   {options[i]}   ");
                    }
                }

                var key = Console.ReadKey(true).Key;
                int lastIndex = options.Length - 1;

                if (key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    return null;
                }

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

        public static (Moedelokale, DateTime)? PickRoomAndDate(string title, DateTime initialDate, int yearSpan, BookingSystem system)
        {
            List<Moedelokale> moedelokaler = system.GetMoedelokaler();

            int day = initialDate.Day;
            int month = initialDate.Month;
            int year = initialDate.Year;
            int roomIndex = 0;

            int activeField = 0;

            int yearMin = DateTime.Today.Year;
            int yearMax = yearMin + yearSpan;
            
            Console.Clear();
            
            while (true)
            {

                Console.SetCursorPosition(0, 0);
                Console.WriteLine((title + "\n").PadRight(50));

                string line = "";

                if (activeField == 0)
                {
                    line = $" > {moedelokaler[roomIndex].navn} /  {day} /  {month} /  {year}";
                }
                
                if (activeField == 1)
                {
                    line = $"   {moedelokaler[roomIndex].navn} /> {day} /  {month} /  {year}";
                }
                
                if (activeField == 2)
                {
                    line = $"   {moedelokaler[roomIndex].navn} /  {day} /> {month} /  {year}";
                }
               
                if (activeField == 3)
                {
                    line = $"   {moedelokaler[roomIndex].navn} /  {day} /  {month} /> {year}";
                }

                Console.WriteLine(line.PadRight(50));

                var key = Console.ReadKey(true).Key;

                int lastFieldIndex = 3;
                int lastRoomIndex = moedelokaler.Count - 1;
                int lastMonth = 12;
                int lastDay = DateTime.DaysInMonth(year, month);
                int lastYear = yearMax;

                if (key == ConsoleKey.Escape)
                {
                    return null;
                }

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

                if (activeField == 0)
                {
                    if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                    {
                        roomIndex--;

                        if (roomIndex < 0)
                        {
                            roomIndex = lastRoomIndex;
                        }
                    }

                    if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                    {
                        roomIndex++;
                        
                        if (roomIndex > lastRoomIndex)
                        {
                            roomIndex = 0;
                        }
                    }
                }

                else if (activeField == 1)
                {
                    if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                    {
                        day++;

                        if (day > lastDay)
                        {
                            day = 1;
                        }
                    }

                    if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                    {
                        day--;

                        if (day < 1)
                        {
                            day = lastDay;
                        }
                    }
                }
                else if (activeField == 2)
                {

                    if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                    {
                        month++;

                        if (month > lastMonth)
                        {
                            month = 1;
                        }

                        int maxDay = DateTime.DaysInMonth(year, month);

                        if (day > maxDay)
                        {
                            day = maxDay;
                        }
                    }

                    if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                    {
                        month--;

                        if (month < 1)
                        {
                            month = lastMonth;
                        }

                        int maxDay = DateTime.DaysInMonth(year, month);

                        if (day > maxDay)
                        {
                            day = maxDay;
                        }
                    }
                }

                else if (activeField == 3)
                {
                    if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                    {
                        year++;

                        if (year > yearMax)
                        {
                            year = yearMax;
                        }

                        int maxDay = DateTime.DaysInMonth(year, month);

                        if (day > maxDay)
                        {
                            day = maxDay;
                        }
                    }

                    if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                    {
                        year--;

                        if (year < yearMin)
                        {
                            year = yearMin;
                        }

                        int maxDay = DateTime.DaysInMonth(year, month);

                        if (day > maxDay)
                        {
                            day = maxDay;
                        }
                    }
                }

                if (key == ConsoleKey.Enter)
                {
                    return (moedelokaler[roomIndex], new DateTime(year, month, day));
                }
            }
        }

        public static (TimeSpan startTid, TimeSpan slutTid)? PickStartTidSlutTid(List<Booking> bookingsValgtLokaleDato, TimeSpan start, TimeSpan end)
        {
            List<TimeSpan> ticks = new List<TimeSpan>();
            for (TimeSpan t = start; t <= end; t = t.Add(TimeSpan.FromMinutes(30)))
            {
                ticks.Add(t); 
            }

            bool[] blocked = new bool[ticks.Count];

            for (int i = 0; i < ticks.Count; i++)
            {
                TimeSpan t = ticks[i];

                foreach (Booking b in bookingsValgtLokaleDato)
                {
                    if (b.startTid <= t && t < b.slutTid)
                    {
                        blocked[i] = true; 
                        break; 
                    }
                }
            }

            int selectedIndex = 0;
            int startIndex = -1;
            int endIndex = -1;

            while (selectedIndex < blocked.Length && blocked[selectedIndex])
            {
                selectedIndex++;
            }
            if (selectedIndex >= blocked.Length)
            {
                Console.WriteLine("INGEN LEDIGE TIDER PAA DEN VALGTE DATO");
                Console.ReadKey();
                return null;
            }

            Console.Clear(); 

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                
                for (int i = 0; i < ticks.Count; i++)
                {
                    

                        if (i == selectedIndex)
                        {   
                            if (i == startIndex || i == endIndex)
                            {
                                Console.WriteLine($">| + |{ticks[i]:hh\\:mm}");
                            }
                            else if (blocked[i] == true)
                            {
                                Console.WriteLine($">| X |{ticks[i]:hh\\:mm}");
                            }
                            else 
                            {
                                Console.WriteLine($">|   |{ticks[i]:hh\\:mm}");
                            }

                        }
                        else
                        {
                            if (i == startIndex || i == endIndex)
                            {
                                Console.WriteLine($" | + |{ticks[i]:hh\\:mm}");
                            }
                            else if (blocked[i] == true)
                            {
                                Console.WriteLine($" | X |{ticks[i]:hh\\:mm}");
                            }
                            else
                            {
                                Console.WriteLine($" |   |{ticks[i]:hh\\:mm}");
                            }
                        }

                    
                }

                var key = Console.ReadKey(true).Key;
                int lastIndex = ticks.Count - 1;
                
                if (key == ConsoleKey.Escape)
                {
                    return null;
                }

                if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    selectedIndex--;

                    if (selectedIndex < 0)
                    {
                        selectedIndex = lastIndex;
                    }
                    while (blocked[selectedIndex] == true)
                    {
                        selectedIndex--;
                        if (selectedIndex < 0)
                        {
                            selectedIndex = lastIndex;
                        }
                    }
                }

                if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    selectedIndex++;

                    if (selectedIndex > lastIndex)
                    {
                        selectedIndex = 0;
                    }
                    while (blocked[selectedIndex] == true)
                    {
                        selectedIndex++;
                        if (selectedIndex > lastIndex)
                        {
                            selectedIndex = 0;
                        }
                    }
                }
                
                if (key == ConsoleKey.Enter)
                {
                    if (startIndex == -1)
                    {
                        startIndex = selectedIndex;
                    }
                    else if (startIndex == selectedIndex)
                    {
                        startIndex = -1;
                    }
                    else if (endIndex == -1 && selectedIndex > startIndex)
                    {
                        TimeSpan startTime = ticks[startIndex];
                        TimeSpan proposedEnd = ticks[selectedIndex];

                        bool conflict = false;

                        foreach (Booking b in bookingsValgtLokaleDato)
                        {
                            if (startTime < b.slutTid && proposedEnd > b.startTid)
                            {
                                conflict = true;
                                break;
                            }
                        }
                        
                        if (conflict == true)
                        {
                            Console.Clear();
                            Console.WriteLine("ANDRE BOOKINGER KAN IKKE OVERLAPPES");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            endIndex = selectedIndex;
                        }
                        
                    }
                    else if (endIndex == selectedIndex)
                    {
                        endIndex = -1;
                    }
                    else if (endIndex == -1 && selectedIndex <= startIndex)
                    {
                        endIndex = -1;

                        Console.Clear();
                        Console.WriteLine("SLUTTID KAN IKKE VAERE FOER STARTTID");
                        Console.ReadKey();
                        Console.Clear();

                        continue;
                    }

                    if (startIndex != -1 && endIndex != -1)
                    {
                        Console.Clear();

                        string[] tidConfirmation =
                        {
                               "JA",
                               "NEJ",
                        };

                        int? choiceConfirmTid = ConsoleHelpers.ChooseFromListOrCancel($"STARTTID: {ticks[startIndex]:hh\\:mm}\nSLUTTID: {ticks[endIndex]:hh\\:mm} \n\nBEKRAEFT?", tidConfirmation);
                        
                        if (choiceConfirmTid == 0)
                        {

                            return (ticks[startIndex], ticks[endIndex]);
                        }
                        else
                        {
                            endIndex = -1;
                            Console.Clear();
                            continue;
                        }
                    }
                }
            }
        }
    }
}
