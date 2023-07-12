using BreadersHomebook.Controllers;

namespace BreadersHomebook
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var consoleController = new ConsoleController();

            consoleController.Run();
        }
    }
}