using System;
using System.Collections.Generic;
using System.Linq;
using BreadersHomebook.Models;
using BreadersHomebook.Services;
using static System.Console;

namespace BreadersHomebook.Controllers
{
    
    public class ConsoleController
    {
        public bool IsRunning { get; set; }
        
        private readonly Helper _helper;

        private readonly CommandExecutor _commandExecutor;

        public ConsoleController()
        {
            _helper = new Helper();
            _commandExecutor = new CommandExecutor();
        }

        public void Run()
        {
            IsRunning = true;
            _helper.PrintWelcome();
            while (IsRunning)
            {
                try
                {
                    var command = _helper.RequestCommandInput();
                    _commandExecutor.Execute(command.Trim());
                }
                catch (ExitException exit)
                {
                    IsRunning = false;
                }
            }
        }
    }
}