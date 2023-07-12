using System.Collections.Generic;
using BreadersHomebook.Models;
using static System.Console;

namespace BreadersHomebook.Services
{
    public class CommandExecutor
    {
        private readonly DatabaseManager _databaseManager = new DatabaseManager();
        private readonly EnumParser _enumParser = new EnumParser();
        private readonly Helper _helper = new Helper();
        private readonly Utils _utils = new Utils();

        public void Execute(string command)
        {
            switch (command)
            {
                case "show all works":
                    _databaseManager.GetAllWorks().ForEach(work => work.Print());
                    break;
                case "show list":
                    var filters = RequestFilters();
                    var list = _databaseManager.GetWorksByFilter(filters);
                    if (list.Count == 0) WriteLine("No work with filters {0} has been found", filters);
                    list.ForEach(work => work.Print());
                    break;
                case "exit":
                    throw new ExitException();
                case "help":
                    _helper.PrintHelp();
                    break;
                case "help filter keys":
                    _helper.PrintHelpFilterKeys();
                    break;
                case "help filter values":
                    _helper.PrintHelpFilterValuesForKey();
                    break;
                default:
                    WriteLine("Entered unknown command, enter help to see supported commands");
                    break;
            }
        }

        private FilterModel RequestFilters()
        {
            WriteLine("Use filters? Y/N");
            var response = ReadLine();
            var filters = new FilterModel();

            response = response == null ? "" : response.ToUpper().Trim();

            switch (response)
            {
                case "Y":
                    filters = ConstructFilters(filters);
                    return filters;
                case "N":
                    return filters;
                case "EXIT":
                    throw new ExitException();
                default:
                    WriteLine("Please enter Y for yes or N for no");
                    return RequestFilters();
            }
        }

        private FilterModel ConstructFilters(FilterModel filters)
        {
            var isAddingFilters = true;
            while (isAddingFilters)
            {
                WriteLine("Enter filter key. To see list with selected filters enter: find");
                var filterKey = ReadLine();
                var key = filterKey == null ? "" : filterKey.Trim();

                if (key.Equals("help"))
                {
                    _helper.PrintHelpForFilter();
                    continue;
                }

                if (key.Equals("help filter keys"))
                {
                    _helper.PrintHelpFilterKeys();
                    continue;
                }

                if (key.Equals("help filter values"))
                {
                    _helper.PrintHelpFilterValuesForKey();
                    continue;
                }

                switch (key)
                {
                    case "variety":

                        WriteLine("Enter culture variety name:");
                        var variety = ReadLine();

                        if (variety == null)
                        {
                            WriteLine("Variety name can't be empty");
                            break;
                        }

                        filters.VarietyName = variety.Trim();
                        break;

                    case "author":

                        WriteLine("Enter author name:");
                        var author = ReadLine();

                        if (author == null)
                        {
                            WriteLine("Author name can't be empty");
                            break;
                        }

                        filters.Author = author.Trim();
                        break;

                    case "parents":

                        WriteLine("Enter variety parents");
                        var parents = ReadLine();
                        var parentsArr = _utils.SeparateStringArr(parents);

                        if (parentsArr.Length == 0)
                        {
                            WriteLine("Filter must have at least one parent variety");
                            break;
                        }

                        filters.ParentVarieties = new List<string>(parentsArr);
                        break;

                    case "minProductivity":

                        WriteLine("Enter minimum productivity, kg for plant in one season");
                        var min = ReadLine();
                        min = min ?? "0";
                        var minValue = decimal.Parse(min);

                        if (minValue < 0)
                        {
                            WriteLine("Productivity can't be negative");
                            break;
                        }

                        filters.MinProductivity = minValue;
                        break;

                    case "maxProductivity":

                        WriteLine("Enter maximum productivity, kg for plant in one season");
                        var max = ReadLine();
                        max = max ?? decimal.MaxValue.ToString();
                        var maxValue = decimal.Parse(max);

                        if (maxValue < 0)
                        {
                            WriteLine("Productivity can't be negative");
                            break;
                        }

                        filters.MaxProductivity = maxValue;
                        break;

                    case "fruitCharacteristics":

                        WriteLine("Enter fruit characteristics");
                        var characteristics = ReadLine();
                        characteristics = characteristics == null ? "" : characteristics.Trim();
                        var characteristicsArr = _utils.SeparateStringArr(characteristics);
                        if (characteristicsArr.Length == 0)
                        {
                            WriteLine("Filter must have at least one characteristic");
                            break;
                        }

                        foreach (var characteristic in characteristicsArr)
                        {
                            var fruitCharacteristic =
                                _enumParser.ParseFruitCharacteristics(characteristic.Trim());
                            filters.FruitCharacteristics.Add(fruitCharacteristic);
                        }

                        break;

                    case "frostResistances":

                        WriteLine("Enter frost resistances:");
                        var frostResistances = ReadLine();
                        frostResistances = frostResistances == null ? "" : frostResistances.Trim();
                        var frostResistancesArr = _utils.SeparateStringArr(frostResistances);
                        if (frostResistancesArr.Length == 0)
                        {
                            WriteLine("Filter must have at list one element");
                            break;
                        }

                        foreach (var resistance in frostResistancesArr)
                            try
                            {
                                var frostResistance = _enumParser.ParseFrostResistances(resistance);
                                filters.FrostResistances.Add(frostResistance);
                            }
                            catch (ParsingException e)
                            {
                                WriteLine(e.Message);
                            }

                        break;

                    case "pestsResistances":
                        WriteLine("Enter pests resistances:");
                        var pestsResistances = ReadLine();
                        pestsResistances = pestsResistances == null ? "" : pestsResistances.Trim();
                        var pestsResistancesArr = _utils.SeparateStringArr(pestsResistances);
                        if (pestsResistancesArr.Length == 0)
                        {
                            WriteLine("Filter must have at list one element");
                            break;
                        }

                        foreach (var resistance in pestsResistancesArr)
                        {
                            var pestsResistance = _enumParser.ParsePestsResistances(resistance);
                            filters.PestsResistances.Add(pestsResistance);
                        }

                        break;

                    case "diseasesResistances":
                        WriteLine("Enter diseases resistances:");
                        var diseasesResistances = ReadLine();
                        diseasesResistances = diseasesResistances == null ? "" : diseasesResistances.Trim();
                        var diseasesResistancesArr = _utils.SeparateStringArr(diseasesResistances);
                        if (diseasesResistancesArr.Length == 0)
                        {
                            WriteLine("Filter must have at list one element");
                            break;
                        }

                        foreach (var resistance in diseasesResistancesArr)
                        {
                            var pestsResistance = _enumParser.ParseDesiasesResistances(resistance);
                            filters.DiseasesResistances.Add(pestsResistance);
                        }

                        break;

                    case "fond":
                        WriteLine("Enter fond name: ");
                        var fond = ReadLine();
                        fond = fond == null ? "" : fond.Trim();
                        filters.Fond = fond;
                        break;

                    case "exit":
                        throw new ExitException();

                    case "find":
                        isAddingFilters = false;
                        break;

                    default:
                        WriteLine("Filter name {0} not supported.", key);
                        break;
                }
            }

            return filters;
        }
    }
}