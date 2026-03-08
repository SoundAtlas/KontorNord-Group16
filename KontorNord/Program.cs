namespace KontorNord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Room> rooms = new List<Room>();
            List<Booking> bookings = new List<Booking>();

            rooms.Add(new Room("Alpha", 10));
            rooms.Add(new Room("Beta", 8));
            rooms.Add(new Room("Gamma", 20));

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
                        Console.WriteLine("Room: " + room.Name + " Capacity: " + room.Capacity);
                    }

                }

                else if (choice == "2")
                {
                    Console.WriteLine("Enter Room Name: ");
                    string name = Console.ReadLine();

                    Console.WriteLine("Enter Capacity: ");
                    int capacity = int.Parse(Console.ReadLine());

                    Room newRoom = new Room(name, capacity);
                    rooms.Add(newRoom);

                    Console.WriteLine("Room Added Successfully!");
                }

                else if (choice == "3")
                {
                    Console.WriteLine("Enter room name:");
                    string roomName = Console.ReadLine();

                    bool roomExists = false;

                    foreach (Room room in rooms)
                    {
                        if (room.Name == roomName)
                        {
                            roomExists = true;
                        }
                    }

                    if (roomExists)
                    {
                        Console.WriteLine("Enter your name:");
                        string bookedBy = Console.ReadLine();

                        Console.WriteLine("Enter date:");
                        string date = Console.ReadLine();

                        bool alreadyBooked = false;

                        foreach (Booking booking in bookings)
                        {
                            if (booking.RoomName == roomName && booking.Date == date)
                            {
                                alreadyBooked = true;
                            }
                        }

                        if (alreadyBooked)
                        {
                            Console.WriteLine("This room is already booked on that date.");
                        }
                        else
                        {
                            int bookingId = bookings.Count + 1;
                            
                            Booking newBooking = new Booking(bookingId, roomName, bookedBy, date);
                            bookings.Add(newBooking);

                            Console.WriteLine("Booking added!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Room does not exist.");
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
                                              " | Date: " + booking.Date);
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

