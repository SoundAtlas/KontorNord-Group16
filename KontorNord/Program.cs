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

            foreach (Room room in rooms)
            {
                Console.WriteLine("Room: "+ room.Name + "Capacity: " + room.Capacity);

        }
    }
}
