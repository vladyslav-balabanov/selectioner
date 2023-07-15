using BreadersHomebook.Exceptions;
using BreadersHomebook.Services;
using BreadersHomebook.Utils;

namespace BreadersHomebook.Controllers
{
    public class ConsoleController
    {
        private readonly CommandExecutor _commandExecutor;

        private readonly Helper _helper;

        public ConsoleController()
        {
            _helper = new Helper();
            _commandExecutor = new CommandExecutor();
        }

        public bool IsRunning { get; set; }

        public void Run()
        {
            IsRunning = true;
            _helper.PrintWelcome();
            while (IsRunning)
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