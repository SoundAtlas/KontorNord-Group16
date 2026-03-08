using System;
using System.Collections.Generic;
using System.Text;

namespace KontorNord
{
    internal class Room
    {
       
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Capacity { get; set; }

        public Room(int id, string name, int capacity) 
        {

            Id = id;
            Name= name;
            Capacity = capacity;
        }
    }
}
