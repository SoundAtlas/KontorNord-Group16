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
                Console.WriteLine("2 - Exit");


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

