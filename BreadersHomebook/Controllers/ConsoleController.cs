using System;
using System.Collections.Generic;
using System.Linq;
using BreadersHomebook.Models;
using static System.Console;

namespace BreadersHomebook.Controllers
{
    
    public class ConsoleController
    {
        public bool IsRunning { get; set; }
        private DatabaseManager DatabaseManager;

        public ConsoleController(DatabaseManager databaseManager)
        {
            DatabaseManager = databaseManager;
        }

        public void Run()
        {
            IsRunning = true;
            PrintWelcomingInstructions();
            while (IsRunning)
            {
                var command = RequestCommandInput();
                ExecuteCommand(command);
            }
        }

        private void PrintWelcomingInstructions()
        {
            WriteLine("Welcome to breeder's homebook!");
            WriteLine("For list of supported commands enter command: help");
        }

        private void PrintHelpInstruction()
        {
            WriteLine("Supported commands:");
            WriteLine("show list - prints list of all breeder's works");
            WriteLine("show list filtered by <filter1 key>:<filter1 value>; <filter2 key>:<filter2 value1>,<filter2 value2> - prints list of breeder's works that match filters");
            WriteLine("help filter keys - prints list of filter keys");
            WriteLine("help filter values <key> - prints list of filter values for entered key");
            WriteLine("exit - exits program");
        }

        private void PrintKeysHelp()
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

        private string RequestCommandInput()
        {
            Out.WriteLine("Please, enter command");
            string command = In.ReadLine();
            return command;
        }

        private void ExecuteCommand(string command)
        {
            string showFiltered = "show list filtered by ";
            string show = "show list";
            
            if (command.Trim().StartsWith(showFiltered))
            {
                string filters = command.Substring(showFiltered.Length).Trim();
                var filtersArr = filters.Split(';');
                var filterMap = ConstructFilterMap(filtersArr);
                var filterModel = ComposeFilterModel(filterMap);
                WriteLine("Filtered works:");
                PrintList(DatabaseManager.GetWorksByFilter(filterModel));
            }
            else if (command.Trim().Equals(show))
            {
                WriteLine("All works:");
                PrintList(DatabaseManager.GetAllWorks());
            }
            else if (command.Trim().Equals("exit"))
            {
                IsRunning = false;
            }
            else if (command.Trim().Equals("fill db"))
            {
                // var item1 = new SelectionistsWorkModel();
                // item1.NameOfCultureVariety = "Alvnia Gala apple tree";
                // item1.Author = "Fankhauser";
                // item1.ParentVarieties = new List<string>() { "Gala apple tree" };
                // item1.FruitCharacteristics = new List<FruitCharacteristics>()
                // {
                //     FruitCharacteristics.JuicyWallStructure,
                //     FruitCharacteristics.SimpleFruit,
                //     FruitCharacteristics.PolyCotyledon,
                //     FruitCharacteristics.NonOpeningFruit,
                // };
                // item1.Productivity = 50;
                // item1.DesiasesResistance = DesiasesResistances.Middle;
                // item1.FrostResistance = FrostResistances.Middle;
                // item1.PestsResistance = PestsResistances.Middle;
                // item1.FondsWithCulture = new List<string>()
                // {
                //     "Fankhauser Apples company",
                // };
                // DatabaseManager.AddWork(item1);
                
                var item2 = new SelectionistsWorkModel();
                item2.NameOfCultureVariety = "Gala apple tree";
                item2.Author = "McKenzie";
                item2.ParentVarieties = new List<string>() { "Kidd's Orange Red apple tree", "Golden Delicious apple tree" };
                item2.FruitCharacteristics = new List<FruitCharacteristics>()
                {
                    FruitCharacteristics.JuicyWallStructure,
                    FruitCharacteristics.SimpleFruit,
                    FruitCharacteristics.PolyCotyledon,
                    FruitCharacteristics.NonOpeningFruit,
                };
                item2.Productivity = 60;
                item2.DesiasesResistance = DesiasesResistances.Middle;
                item2.FrostResistance = FrostResistances.Middle;
                item2.PestsResistance = PestsResistances.Middle;
                item2.FondsWithCulture = new List<string>();
                DatabaseManager.AddWork(item2);
            }
            else if (command.Trim().Equals("help"))
            {
                PrintHelpInstruction();
            }
            else if (command.Trim().Equals("help filter keys"))
            {
                PrintKeysHelp();
            }
            else if (command.Trim().StartsWith("help filter values "))
            {
                var key = command.Trim().Substring("help filter values ".Length).Trim();
            }
            else
            {
                WriteLine("Entered unknown command. Enter help to see supported commands.");
            }
        }

        private Dictionary<string, List<string>> ConstructFilterMap(string[] filters)
        {
            var filtersMap = new Dictionary<string, List<string>>();
            foreach (var filter in filters)
            {
                var filterStringSplit = filter.Trim().Split(':');
                var filterString = filterStringSplit[1];
                var filterList = filterString.Split(',').ToList();
                filtersMap.Add(filterStringSplit[0], filterList);
            }

            return filtersMap;
        }

        private FilterModel ComposeFilterModel(Dictionary<string, List<string>> filterMap)
        {
            var filterModel = new FilterModel();
            filterModel.MaxProductivity = decimal.MaxValue;
            filterModel.MinProductivity = decimal.MinValue;
            foreach (var key in filterMap.Keys)
            {
                switch (key)
                {
                    case "variety":
                        foreach (var filterString in filterMap[key])
                        {
                            filterModel.VarietyName = filterString;
                        }
                        break;
                    case "author":
                        foreach (var filterString in filterMap[key])
                        {
                            filterModel.Author = filterString;
                        }
                        break;
                    case "parents":
                        foreach (var filterString in filterMap[key])
                        {
                            filterModel.ParentVarieties.Add(filterString);
                        }
                        break;
                    case "minProductivity":
                        foreach (var filterString in filterMap[key])
                        {
                            filterModel.MinProductivity = decimal.Parse(filterString);
                        }
                        break;
                    case "maxProductivity":
                        foreach (var filterString in filterMap[key])
                        {
                            filterModel.MaxProductivity = decimal.Parse(filterString);
                        }
                        break;
                    case "fruitCharacteristics":
                        foreach (var filterString in filterMap[key])
                        {
                            FruitCharacteristics fruitCharacteristic;
                            switch (filterString)
                            {
                                case "simple":
                                    fruitCharacteristic = FruitCharacteristics.SimpleFruit;
                                    break;
                                case "complex":
                                    fruitCharacteristic = FruitCharacteristics.ComplexFruit;
                                    break;
                                case "dry":
                                    fruitCharacteristic = FruitCharacteristics.DryWallStructure;
                                    break;
                                case "juicy":
                                    fruitCharacteristic = FruitCharacteristics.JuicyWallStructure;
                                    break;
                                case "mono":
                                    fruitCharacteristic = FruitCharacteristics.MonoCotyledon;
                                    break;
                                case "poly":
                                    fruitCharacteristic = FruitCharacteristics.PolyCotyledon;
                                    break;
                                case "opening":
                                    fruitCharacteristic = FruitCharacteristics.OpeningFruit;
                                    break;
                                case "nonOpening":
                                    fruitCharacteristic = FruitCharacteristics.NonOpeningFruit;
                                    break;
                                default:
                                    throw new Exception("Fruit characteristic is not found: " + filterString);
                            }
                            filterModel.FruitCharacteristics.Add(fruitCharacteristic);
                        }
                        break;
                    case "frostResistances":
                        foreach (var filterString in filterMap[key])
                        {
                            FrostResistances frostResistance;
                            switch (filterString)
                            {
                                case "none":
                                    frostResistance = FrostResistances.None;
                                    break;
                                case "low":
                                    frostResistance = FrostResistances.Low;
                                    break;
                                case "middle":
                                    frostResistance = FrostResistances.Middle;
                                    break;
                                case "high":
                                    frostResistance = FrostResistances.High;
                                    break;
                                case "complete":
                                    frostResistance = FrostResistances.Complete;
                                    break;
                                default:
                                    throw new Exception("Frost resistance is not found: " + filterString);
                            }
                            filterModel.FrostResistances.Add(frostResistance);
                        }
                        break;
                    case "pestsResistances":
                        foreach (var filterString in filterMap[key])
                        {
                            PestsResistances pestsResistance;
                            switch (filterString)
                            {
                                case "none":
                                    pestsResistance = PestsResistances.None;
                                    break;
                                case "low":
                                    pestsResistance = PestsResistances.Low;
                                    break;
                                case "middle":
                                    pestsResistance = PestsResistances.Middle;
                                    break;
                                case "high":
                                    pestsResistance = PestsResistances.High;
                                    break;
                                case "complete":
                                    pestsResistance = PestsResistances.Complete;
                                    break;
                                default:
                                    throw new Exception("Pest resistance is not found: " + filterString);
                            }
                            filterModel.PestsResistances.Add(pestsResistance);
                        }
                        break;
                    case "desiasesResistances":
                        foreach (var filterString in filterMap[key])
                        {
                            DesiasesResistances desiasesResistances;
                            switch (filterString)
                            {
                                case "none":
                                    desiasesResistances = DesiasesResistances.None;
                                    break;
                                case "low":
                                    desiasesResistances = DesiasesResistances.Low;
                                    break;
                                case "middle":
                                    desiasesResistances = DesiasesResistances.Middle;
                                    break;
                                case "high":
                                    desiasesResistances = DesiasesResistances.High;
                                    break;
                                case "complete":
                                    desiasesResistances = DesiasesResistances.Complete;
                                    break;
                                default:
                                    throw new Exception("Desiase resistance is not found: " + filterString);
                            }
                            filterModel.DesiasesResistances.Add(desiasesResistances);
                        }
                        break;
                    case "fond":
                        foreach (var filterString in filterMap[key])
                        {
                            filterModel.Fond = filterString;
                        }
                        break;
                    default:
                        throw new Exception("Filter name " + key + " not found.");
                }
            }

            return filterModel;
        }

        private void PrintList(List<SelectionistsWorkModel> workModels)
        {
            workModels.ForEach(work =>
                work.Print());
        }
    }
}