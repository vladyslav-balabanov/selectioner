using static System.Console;

namespace BreadersHomebook.Services
{
    public class Helper
    {
        public void PrintHelp()
        {
            WriteLine("Supported commands:");
            WriteLine("show all works - prints list of all breeders' works");
            WriteLine("show list - prints list of breeder's works that match filters");
            WriteLine("help filter keys - prints list of filter keys");
            WriteLine("help filter values - prints list of filter values for entered key");
            WriteLine("exit - exits program");
        }

        public void PrintHelpFilterKeys()
        {
            WriteLine("Supported keys:");
            WriteLine("variety");
            WriteLine("author");
            WriteLine("parents");
            WriteLine("minProductivity");
            WriteLine("maxProductivity");
            WriteLine("fruitCharacteristics");
            WriteLine("frostResistances");
            WriteLine("pestsResistances");
            WriteLine("desiasesResistances");
            WriteLine("fond");   
        }

        public void PrintHelpFilterValuesForKey()
        {
            WriteLine("Enter key about which values you want to know: ");
            string key = ReadLine();
            key = key == null ? "" : key.Trim();

            if (key.Equals("exit"))
            {
                throw new ExitException();
            }
            
            Write("Values for key {0}: ",key);
                switch (key)
                {
                    case "variety":
                        Write(" of culture variety. Example: variety:Gala apple tree");
                        break;
                    case "author":
                        Write(" of culture variety. Example: author:Fankhauser");
                        break;
                    case "parents":
                        Write(" of culture. Example: parents:Kidd's Orange Red apple tree,Golden Delicious apple tree");
                        break;
                    case "minProductivity":
                        Write(" of culture in kg per tree per season. Example: minProductivity:3.45");
                        break;
                    case "maxProductivity":
                        Write(" of culture in kg per tree per season. Example: maxProductivity:257.07");
                        break;
                    case "fruitCharacteristics":
                        Write(" of culture. Example: fruitCharacteristics:Kidd's Orange Red apple tree,Golden Delicious apple tree");
                        break;
                    case "frostResistances":
                        Write(" of culture. Example: frostResistances:high,medium");
                        break;
                    case "pestsResistances":
                        Write(" of culture. Example: pestsResistances:low,medium");
                        break;
                    case "desiasesResistances":
                        Write(" of culture. Example: desiasesResistances:high,complete");
                        break;
                    case "fond":
                        Write(", where varieties are kept. Example: fond:Fankhauser Apples company");
                        break;
                    default:
                        Write(" are not found. Key is not supported");
                        break;
                }
                WriteLine();
        }

        public void PrintWelcome()
        {
            WriteLine("Welcome to breeder's homebook!");
            WriteLine("For list of supported commands enter command: help");
        }
        
        public string RequestCommandInput()
        {
            Out.WriteLine("Please, enter command");
            string command = In.ReadLine();
            return command;
        }
    }
}