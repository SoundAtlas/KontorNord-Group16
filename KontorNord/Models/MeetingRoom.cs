using System;
namespace KontorNord.Models
{
    // Class representing a meeting room in the system - contains properties for the room's ID and name
    public class MeetingRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
