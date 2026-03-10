using System;

namespace SimpleBookingSystem
{
    class Program
    {
        static BookingManager manager = new BookingManager(); // Opretter manageren

        static void Main(string[] args)
        {
            bool running = true;

            // Menuen kører så længe running = true
            while (running)
            {
                Console.WriteLine("\n--- Booking System ---");
                Console.WriteLine("1. Opret booking");
                Console.WriteLine("2. Se bookinger");
                Console.WriteLine("3. Rediger booking");
                Console.WriteLine("4. Slet booking");
                Console.WriteLine("5. Afslut");
                Console.Write("Vælg: ");

                string choice = Console.ReadLine();

                if (choice == "1") CreateBooking();
                else if (choice == "2") ShowBookings();
                else if (choice == "3") EditBooking();
                else if (choice == "4") DeleteBooking();
                else if (choice == "5") running = false;
            }
        }

        // Opretter en booking
        static void CreateBooking()
        {
            Console.WriteLine("\nVælg lokale:");
            foreach (var r in manager.rooms)
            {
                Console.WriteLine($"{r.Id}. {r.Name}");
            }

            Console.Write("Lokale: ");
            int roomId = int.Parse(Console.ReadLine());
            var room = manager.FindRoom(roomId);

            Console.Write("Starttid (yyyy-mm-dd hh:mm): ");
            DateTime start = DateTime.Parse(Console.ReadLine());

            Console.Write("Sluttid (yyyy-mm-dd hh:mm): ");
            DateTime end = DateTime.Parse(Console.ReadLine());

            // Tjek om lokalet er ledigt
            if (!manager.IsRoomFree(room, start, end))
            {
                Console.WriteLine("Lokalet er optaget.");
                return;
            }

            Console.Write("Note: ");
            string note = Console.ReadLine();

            manager.CreateBooking(room, start, end, note);
            Console.WriteLine("Booking oprettet!");
        }

        // Viser alle bookinger
        static void ShowBookings()
        {
            Console.WriteLine("\n--- Bookinger ---");

            foreach (var b in manager.bookings)
            {
                Console.WriteLine($"ID: {b.Id} | {b.Room.Name} | {b.Start} - {b.End} | Note: {b.Note}");
            }
        }

        // Redigerer en booking (kun note)
        static void EditBooking()
        {
            ShowBookings();

            Console.Write("Indtast ID: ");
            int id = int.Parse(Console.ReadLine());

            var b = manager.FindBooking(id);

            if (b == null)
            {
                Console.WriteLine("Booking findes ikke.");
                return;
            }

            Console.Write("Ny note (tom = ingen ændring): ");
            string note = Console.ReadLine();
            if (note != "") b.Note = note;

            Console.WriteLine("Booking opdateret.");
        }

        // Sletter en booking
        static void DeleteBooking()
        {
            ShowBookings();

            Console.Write("Indtast ID: ");
            int id = int.Parse(Console.ReadLine());

            var b = manager.FindBooking(id);

            if (b == null)
            {
                Console.WriteLine("Booking findes ikke.");
                return;
            }

            manager.DeleteBooking(b);
            Console.WriteLine("Booking slettet.");
        }
    }
}