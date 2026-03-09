namespace KontorNord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Room> rooms = new List<Room>();
            List<Booking> bookings = new List<Booking>();

            rooms.Add(new Room(1, "Alpha", 10));
            rooms.Add(new Room(2, "Beta", 8));
            rooms.Add(new Room(3, "Gamma", 20));

            bool running = true;

            while (running)
            {
                Console.Clear();
                ShowMenu();

                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    ShowRooms(rooms);
                    Pause();
                }
                else if (choice == "2")
                {
                    AddRoom(rooms);
                    Pause();
                }
                else if (choice == "3")
                {
                    AddBooking(rooms, bookings);
                    Pause();
                }
                else if (choice == "4")
                {
                    ShowBookings(bookings);
                    Pause();
                }
                else if (choice == "5")
                {
                    DeleteBooking(bookings);
                    Pause();
                }
                else if (choice == "6")
                {
                    running = false;
                    Console.WriteLine("Program Closed.");
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                    Pause();
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("     KONTORNORD BOOKING SYSTEM   ");
            Console.WriteLine("=================================");
            Console.WriteLine("1 - Show Rooms");
            Console.WriteLine("2 - Add Room");
            Console.WriteLine("3 - Add Booking");
            Console.WriteLine("4 - Show Bookings");
            Console.WriteLine("5 - Delete Booking");
            Console.WriteLine("6 - Exit");
            Console.WriteLine("=================================");
            Console.Write("Choose an option: ");
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void ShowRooms(List<Room> rooms)
        {
            Console.WriteLine();
            Console.WriteLine("----- ROOMS -----");

            foreach (Room room in rooms)
            {
                Console.WriteLine("ID: " + room.Id + " | Room: " + room.Name + " | Capacity: " + room.Capacity);
            }
        }

        static void ShowBookings(List<Booking> bookings)
        {
            Console.WriteLine();
            Console.WriteLine("----- BOOKINGS -----");

            if (bookings.Count == 0)
            {
                Console.WriteLine("No Bookings Found.");
            }
            else
            {
                foreach (Booking booking in bookings)
                {
                    Console.WriteLine("ID: " + booking.Id +
                                      " | Room: " + booking.RoomName +
                                      " | Booked by: " + booking.BookedBy +
                                      " | Date: " + booking.Date +
                                      " | Start: " + booking.StartTime.ToString(@"hh\:mm") +
                                      " | End: " + booking.EndTime.ToString(@"hh\:mm"));
                }
            }
        }

        static void AddRoom(List<Room> rooms)
        {
            Console.WriteLine();
            Console.WriteLine("----- ADD ROOM -----");

            Console.WriteLine("Enter Room Name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Room name cannot be empty.");
                return;
            }

            Console.WriteLine("Enter Capacity: ");
            string? capacityInput = Console.ReadLine();

            if (!int.TryParse(capacityInput, out int capacity))
            {
                Console.WriteLine("Invalid capacity.");
                return;
            }

            int roomId = rooms.Count + 1;

            Room newRoom = new Room(roomId, name, capacity);
            rooms.Add(newRoom);

            Console.WriteLine("Room Added Successfully!");
        }

        static void DeleteBooking(List<Booking> bookings)
        {
            Console.WriteLine();
            Console.WriteLine("----- DELETE BOOKING -----");

            Console.WriteLine("Enter Booking ID To Delete:");
            string? idInput = Console.ReadLine();

            if (!int.TryParse(idInput, out int idToDelete))
            {
                Console.WriteLine("Invalid booking ID.");
                return;
            }

            Booking? bookingToDelete = null;

            foreach (Booking booking in bookings)
            {
                if (booking.Id == idToDelete)
                {
                    bookingToDelete = booking;
                    break;
                }
            }

            if (bookingToDelete != null)
            {
                bookings.Remove(bookingToDelete);
                Console.WriteLine("Booking Deleted!");
            }
            else
            {
                Console.WriteLine("Booking Not Found.");
            }
        }

        static void AddBooking(List<Room> rooms, List<Booking> bookings)
        {
            Console.WriteLine();
            Console.WriteLine("----- ADD BOOKING -----");
            Console.WriteLine("Choose a room by ID:");

            foreach (Room room in rooms)
            {
                Console.WriteLine(room.Id + " - " + room.Name);
            }

            string? roomIdInput = Console.ReadLine();

            if (!int.TryParse(roomIdInput, out int roomId))
            {
                Console.WriteLine("Invalid room ID.");
                return;
            }

            Room? selectedRoom = null;

            foreach (Room room in rooms)
            {
                if (room.Id == roomId)
                {
                    selectedRoom = room;
                    break;
                }
            }

            if (selectedRoom != null)
            {
                string roomName = selectedRoom.Name;

                Console.WriteLine("Enter Your Name:");
                string? bookedBy = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(bookedBy))
                {
                    Console.WriteLine("Name cannot be empty.");
                    return;
                }

                Console.WriteLine("Enter Date:");
                string? date = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(date))
                {
                    Console.WriteLine("Date cannot be empty.");
                    return;
                }

                Console.WriteLine("Enter Start Time (HH:mm):");
                string? startInput = Console.ReadLine();

                if (!TimeSpan.TryParse(startInput, out TimeSpan startTime))
                {
                    Console.WriteLine("Invalid start time.");
                    return;
                }

                Console.WriteLine("Enter End Time (HH:mm):");
                string? endInput = Console.ReadLine();

                if (!TimeSpan.TryParse(endInput, out TimeSpan endTime))
                {
                    Console.WriteLine("Invalid end time.");
                    return;
                }

                if (endTime <= startTime)
                {
                    Console.WriteLine("End time must be later than start time.");
                    return;
                }

                bool alreadyBooked = false;

                foreach (Booking booking in bookings)
                {
                    if (booking.RoomName == roomName && booking.Date == date)
                    {
                        if (startTime < booking.EndTime && endTime > booking.StartTime)
                        {
                            alreadyBooked = true;
                            break;
                        }
                    }
                }

                if (alreadyBooked)
                {
                    Console.WriteLine("This room is already booked on that date.");
                }
                else
                {
                    int bookingId = bookings.Count + 1;

                    Booking newBooking = new Booking(bookingId, roomName, bookedBy, date, startTime, endTime);
                    bookings.Add(newBooking);

                    Console.WriteLine("Booking Added!");
                }
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }
    }
}