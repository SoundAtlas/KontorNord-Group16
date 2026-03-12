using KN.Services;
using KN.UI;

namespace KN
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelpers.ShowTitleScreen(2000);

            BookingSystem system = new BookingSystem();

            Menu.MenuMain(system);

        }
    }
}       
    

