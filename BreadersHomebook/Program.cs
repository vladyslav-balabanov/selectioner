using System;
using System.Net.Configuration;
using BreadersHomebook.Controllers;
using BreadersHomebook.Models;

namespace BreadersHomebook
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ConsoleController consoleController = new ConsoleController(new DatabaseManager());
            
            consoleController.Run();
        }
    }

}