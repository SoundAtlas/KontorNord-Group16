using KontorNord.Models;
using KontorNord.Services;

namespace KontorNord
{
    public class Program
    {
        static void Main()
        {
            // Setup: rooms + service

            var rooms = new List<MeetingRoom>
            {
                new MeetingRoom { Id = 1, Name = "Lokale A" },
                new MeetingRoom { Id = 2, Name = "Lokale B" },
                new MeetingRoom { Id = 3, Name = "Lokale C" }
            };

            var bookingService = new BookingService();


            // --- Main Menu Loop ---

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n === KontorNord Booking System ===");
                Console.WriteLine("1) Opret booking");
                Console.WriteLine("2) Se bookinger");
                Console.WriteLine("0) Exit");
                Console.Write("> ");

                string? choice = Console.ReadLine();

                if (choice == "0")
                   break;

                if (choice == "1")
                    CreateBooking(bookingService, rooms);
                else if (choice == "2")
                    ShowBookings(bookingService, rooms);
                else
                    Console.WriteLine("Ugyldigt valg, prøv igen.");
            }

            // Shows all current bookings

            static void ShowBookings(BookingService bookingService, List<MeetingRoom> rooms)
            {
                var bookings = bookingService.GetAllBookings();

                Console.WriteLine("\n--- Bookinger ---");

                if (bookings.Count == 0)
                {
                    Console.WriteLine("Ingen bookinger fundet.");
                    return;
                }

                foreach (var b in bookings.OrderBy(b => b.Start))
                {
                    string roomName = rooms.First(r => r.Id == b.RoomId).Name;
                    Console.WriteLine($"[{b.Id}] {b.Start:dd-MM-yyyy HH:mm} - {b.End:HH:mm} | {roomName} | {b.BookedBy}");
                }
                Pause();
            }

            // Creates a booking from user input
            static void CreateBooking(BookingService bookingService, List<MeetingRoom> rooms)
            {
                Console.WriteLine("\n--- Opret Booking ---");

                // Show rooms
                Console.WriteLine("Vælg lokale:");
                foreach (var r in rooms)
                    Console.WriteLine($"{r.Id}) {r.Name}");
                

                int roomId = ReadInt("> ", 1, 3);

                // Read date + times
                DateTime date = ReadDate("Dato (yyyy-mm-dd): ");
                TimeSpan startTime = ReadTime("Start tid (HH:mm): ");
                TimeSpan endTime = ReadTime("Slut tid (HH:mm): ");

                // Read name
                string bookedBy = ReadNonEmptyString("Navn: ");

                var booking = new Booking
                {
                    RoomId = roomId,
                    Start = date.Date + startTime,
                    End = date.Date + endTime,
                    BookedBy = bookedBy
                };

                bookingService.AddBooking(booking);
                Console.WriteLine("Booking oprettet!");
                ShowBookings(bookingService, rooms);
                
            }
            
            // --- Input validation methods ---

            static int ReadInt(string message, int min, int max)
            {
                while (true)
                {
                    Console.Write(message);
                    if (int.TryParse(Console.ReadLine(), out int value) &&
                        value >= min &&
                        value <= max)
                        return value;

                    Console.WriteLine($"Ugyldigt input. Skriv et tal mellem {min} og {max}");
                }
            }

            static string ReadNonEmptyString(string message)
            {
                while (true)
                {
                    Console.Write(message);
                    string? s = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(s))
                        return s.Trim();

                    Console.WriteLine("Feltet må ikke være tomt.");
                }
            }

            static DateTime ReadDate(string message)
            {
                while (true)
                {
                    Console.Write(message);
                    if (DateTime.TryParse(Console.ReadLine(), out var date))
                        return date;

                    Console.WriteLine("Ugyldig dato. Eksempel: 2026-03-04");
                }
            }

            static TimeSpan ReadTime(string message)
            {
                while (true)
                {
                    Console.Write(message);
                    if (TimeSpan.TryParse(Console.ReadLine(), out var time))
                        return time;

                    Console.WriteLine("Ugyldigt tidspunkt. Eksempel: 13:30");
                }
            }

            static void Pause()
            {
            Console.Write("\n\nTryk på en vilkårlig tast for at returnere til menu...");
            Console.ReadKey();
            }

        }
    }
}
