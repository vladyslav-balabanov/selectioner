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

        public void PrintHelpForFilter()
        {
            WriteLine("Supported commands:");
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
            WriteLine("diseasesResistances");
            WriteLine("fond");
        }

        public void PrintHelpFilterValuesForKey()
        {
            WriteLine("Enter key about which values you want to know: ");
            var key = ReadLine();
            key = key == null ? "" : key.Trim();

            if (key.Equals("exit")) throw new ExitException();

            if (key.Equals("help filter keys"))
            {
                PrintHelpFilterKeys();
                PrintHelpFilterValuesForKey();
                return;
            }

            if (key.Equals("help"))
            {
                WriteLine("Supported commands:");
                WriteLine("help filter keys - prints supported keys");
                WriteLine("exit - exits application");
                PrintHelpFilterValuesForKey();
                return;
            }

            Write("Values for key {0}: ", key);
            switch (key)
            {
                case "variety":
                    Write("name of culture variety. Example: variety:Gala apple tree");
                    break;
                case "author":
                    Write("author of culture variety. Example: author:Fankhauser");
                    break;
                case "parents":
                    Write(
                        "parent varieties of culture. Example: parents:Kidd's Orange Red apple tree,Golden Delicious apple tree");
                    break;
                case "minProductivity":
                    Write("minimum productivity of culture in kg per tree per season. Example: minProductivity:3.45");
                    break;
                case "maxProductivity":
                    Write("maximum productivity of culture in kg per tree per season. Example: maxProductivity:257.07");
                    break;
                case "fruitCharacteristics":
                    Write(
                        "fruit characteristics of culture. Example: fruitCharacteristics:Kidd's Orange Red apple tree,Golden Delicious apple tree");
                    break;
                case "frostResistances":
                    Write("frost resistance levels of culture. Example: frostResistances:high,medium");
                    break;
                case "pestsResistances":
                    Write("pests resistance levels of culture. Example: pestsResistances:low,medium");
                    break;
                case "diseasesResistances":
                    Write("disease resistance of culture. Example: diseasesResistances:high,complete");
                    break;
                case "fond":
                    Write("fond or farm, where varieties are kept. Example: fond:Fankhauser Apples company");
                    break;
                default:
                    Write("key's values are not found. Key is not supported");
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
            WriteLine("Please, enter command");
            var command = ReadLine();
            return command;
        }
    }
}