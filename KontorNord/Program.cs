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

                Console.WriteLine("1 - Show Rooms");
                Console.WriteLine("2 - Add Room");
                Console.WriteLine("3 - Add Booking");
                Console.WriteLine("4 - Show Bookings");
                Console.WriteLine("5 - Delete Booking");
                Console.WriteLine("6 - Exit");


                string choice = Console.ReadLine();



                if (choice == "1")

                {
                    Console.WriteLine();
                    foreach (Room room in rooms)
                    {
                        Console.WriteLine("ID: " + room.Id + " | Room: " + room.Name + " | Capacity: " + room.Capacity);
                    }

                }

                else if (choice == "2")
                {
                    Console.WriteLine("Enter Room Name: ");
                    string name = Console.ReadLine();

                    Console.WriteLine("Enter Capacity: ");
                    int capacity = int.Parse(Console.ReadLine());

                    int roomId = rooms.Count + 1;

                    Room newRoom = new Room(roomId, name, capacity);
                    rooms.Add(newRoom);

                    Console.WriteLine("Room Added Successfully!");
                }

                else if (choice == "3")
                {
                    Console.WriteLine("Choose a room by ID:");

                    foreach (Room room in rooms)
                    {
                        Console.WriteLine(room.Id + " - " + room.Name);
                    }

                    int roomId = int.Parse(Console.ReadLine());

                    Room? selectedRoom = null;

                    foreach (Room room in rooms)
                    {
                        if (room.Id == roomId)
                        {
                            selectedRoom = room;
                        }
                    }

                    if (selectedRoom != null)
                    {
                        string roomName = selectedRoom.Name;

                        Console.WriteLine("Enter Your Name:");
                        string bookedBy = Console.ReadLine();

                        Console.WriteLine("Enter Date:");
                        string date = Console.ReadLine();

                        Console.WriteLine("Enter Start Time (HH:mm):");
                        TimeSpan startTime = TimeSpan.Parse(Console.ReadLine());

                        Console.WriteLine("Enter End Time (HH:mm):");
                        TimeSpan endTime = TimeSpan.Parse(Console.ReadLine());

                        bool alreadyBooked = false;

                        foreach (Booking booking in bookings)
                        {
                            if (booking.RoomName == roomName && booking.Date == date)
                            {
                                if (startTime < booking.EndTime && endTime > booking.StartTime)
                                {
                                    alreadyBooked = true;
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

                else if (choice == "4")
                {
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

                else if (choice == "5")
                {
                    Console.WriteLine("Enter Booking ID To Delete:");
                    int idToDelete = int.Parse(Console.ReadLine());

                    Booking bookingToDelete = null;

                    foreach (Booking booking in bookings)
                    {
                        if (booking.Id == idToDelete)
                        {
                            bookingToDelete = booking;
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


                else if (choice == "6")

                {
                    running = false;
                    Console.WriteLine("Program Closed.");
                }

                else
                {
                    Console.WriteLine("Invaild Choice. ");
                }

                Console.WriteLine();
            }


        }

    }
}

