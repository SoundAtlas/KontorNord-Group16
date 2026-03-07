namespace KontorNord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Room> rooms = new List<Room>();

            rooms.Add(new Room("Alpha", 10));
            rooms.Add(new Room("Beta", 8));
            rooms.Add(new Room("Gamma", 20));

            bool running = true;

            while (running)

            {

                Console.WriteLine("KontorNord System");
                Console.WriteLine("1 - Show Rooms");
                Console.WriteLine("2 - Add Room");
                Console.WriteLine("3 - Exit");


                string choice = Console.ReadLine();



                if (choice == "1")

                {

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

