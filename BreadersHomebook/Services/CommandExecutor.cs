using System;
using System.Collections.Generic;
using BreadersHomebook.Exceptions;
using BreadersHomebook.Models;
using BreadersHomebook.Utils;
using static System.Console;

namespace BreadersHomebook.Services
{
    public class CommandExecutor
    {
        private readonly DatabaseManager _databaseManager = new DatabaseManager();
        private readonly EnumParser _enumParser = new EnumParser();
        private readonly Helper _helper = new Helper();
        private readonly Utils.Utils _utils = new Utils.Utils();

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
                    WriteLine("Found list of {0}", list.Count);
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
                case "show article":
                    WriteLine("Enter Id of sort that you are interested in");
                    string input = ReadLine();
                    if (int.TryParse(input, out int id))
                    {
                        ArticleModel articleById = _databaseManager.GetArticleById(id);
                        if (articleById == null)
                        {
                            WriteLine("Article for sort with id {0} was not found", id);
                            break;
                        }
                        articleById.Print();
                        break;
                    }
                    WriteLine("Id must be an integer");
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
                try
                {
                    WriteLine("Enter filter key. To see list with selected filters enter: find");

                    var filterKey = ReadLine();
                    if (filterKey == null)
                    {
                        WriteLine("FilterKey must not be null");
                        continue;
                    }
                    filterKey = filterKey.Trim();

                    if (filterKey.Equals("help"))
                    {
                        _helper.PrintHelpForFilter();
                        continue;
                    }

                    if (filterKey.Equals("help filter keys"))
                    {
                        _helper.PrintHelpFilterKeys();
                        continue;
                    }

                    if (filterKey.Equals("help filter values"))
                    {
                        _helper.PrintHelpFilterValuesForKey();
                        continue;
                    }

                    switch (filterKey)
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

                            WriteLine("Enter minimum productivity, kg for hectare in one season");
                            var min = ReadLine();
                            if (int.TryParse(min, out var minValue))
                            {
                                if (minValue < 0)
                                {
                                    WriteLine("Productivity can't be negative");
                                    break;
                                }

                                filters.MinProductivity = minValue;
                                break;
                            }

                            WriteLine("Productivity must be a positive integer");
                            break;
                        
                        case "maxProductivity":

                            WriteLine("Enter maximum productivity, kg for hectare in one season");
                            var max = ReadLine();
                            if (int.TryParse(max, out var maxValue))
                            {
                                if (maxValue < 0)
                                {
                                    WriteLine("Productivity can't be negative");
                                    break;
                                }

                                filters.MaxProductivity = maxValue;
                                break;
                            }

                            WriteLine("Productivity must be a positive integer");
                            break;

                        case "fruitCharacteristics":

                            WriteLine("Enter fruit characteristics");
                            var characteristics = ReadLine();
                            if (characteristics == null)
                            {
                                WriteLine("FruitCharacteristics must not be null");
                                break;
                            }

                            characteristics = characteristics.Trim();
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
                            if (frostResistances == null)
                            {
                                WriteLine("FrostResistances can't be null");
                                break;
                            }

                            frostResistances = frostResistances.Trim();
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
                            if (pestsResistances == null)
                            {
                                WriteLine("PestsResistances must not be null");
                                break;
                            }

                            pestsResistances = pestsResistances.Trim();
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

                        case "diseaseResistances":
                            WriteLine("Enter disease resistances:");
                            var diseaseResistances = ReadLine();
                            if (diseaseResistances == null)
                            {
                                WriteLine("DiseaseResistances can't be null");
                                break;
                            }

                            diseaseResistances = diseaseResistances.Trim();
                            var diseaseResistancesArr = _utils.SeparateStringArr(diseaseResistances);
                            if (diseaseResistancesArr.Length == 0)
                            {
                                WriteLine("Filter must have at list one element");
                                break;
                            }

                            foreach (var resistance in diseaseResistancesArr)
                            {
                                var diseaseResistance = _enumParser.ParseDiseaseResistances(resistance);
                                filters.DiseaseResistances.Add(diseaseResistance);
                            }

                            break;

                        case "fond":
                            WriteLine("Enter fond name: ");
                            var fond = ReadLine();
                            if (fond == null)
                            {
                                WriteLine("Fond name can't be null");
                                break;
                            }
                            fond = fond.Trim();
                            filters.Fond = fond;
                            break;

                        case "exit":
                            throw new ExitException();

                        case "find":
                            isAddingFilters = false;
                            break;

                        default:
                            WriteLine("Filter name {0} not supported.", filterKey);
                            break;
                    }
                }
                catch (ParsingException parsingException)
                {
                    WriteLine(parsingException.Message + " Make sure that it is correct and try again.");
                }
                catch (Exception unexpectedException)
                {
                    WriteLine("Unexpected exception occured, caused by " + unexpectedException.Message + ". Please try again.");
                }
            }

            return filters;
        }
    }
}