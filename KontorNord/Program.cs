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

                Console.WriteLine("KontorNord System");
                Console.WriteLine("1 - Show Rooms");
                Console.WriteLine("2 - Add Room");
                Console.WriteLine("3 - Add Booking");
                Console.WriteLine("4 - Exit");


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

                    Console.WriteLine("Enter Room Name: ");
                    string roomName = Console.ReadLine();

                    Console.WriteLine("Enter Your Name: "); 
                    string bookedBy = Console.ReadLine();

                    Console.WriteLine("Enter Date: ");
                    string date = Console.ReadLine();

                    Booking newBooking = new Booking(roomName, bookedBy, date);
                    bookings.Add(newBooking);

                    Console.WriteLine("Booking Added!");

                }

                
                else if (choice == "4")

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

