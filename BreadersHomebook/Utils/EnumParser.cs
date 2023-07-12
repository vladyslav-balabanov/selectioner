using System;
using BreadersHomebook.Models;

namespace BreadersHomebook.Services
{
    public class EnumParser
    {
        public string DesiasesResistancesKey = "desiasesResistance";
        public string FrostResistancesKey = "frostResistance";
        public string FruitCharacteristicsKey = "fruitCharacteristics";
        public string PestsResistancesKey = "pestsResistance";

        public FruitCharacteristics ParseFruitCharacteristics(string value)
        {
            switch (value)
            {
                case "simple":
                    return FruitCharacteristics.SimpleFruit;
                case "complex":
                    return FruitCharacteristics.ComplexFruit;
                case "dry":
                    return FruitCharacteristics.DryWallStructure;
                case "juicy":
                    return FruitCharacteristics.JuicyWallStructure;
                case "mono":
                    return FruitCharacteristics.MonoCotyledon;
                case "poly":
                    return FruitCharacteristics.PolyCotyledon;
                case "opening":
                    return FruitCharacteristics.OpeningFruit;
                case "nonOpening":
                    return FruitCharacteristics.NonOpeningFruit;
                default:
                    Console.WriteLine(
                        "Fruit characteristic {0} is not valid, to see valid fruit characteristics enter command: help filter values fruitCharacteristics",
                        value);
                    throw new ParsingException("Could not parse FruitCharacteristics " + value);
            }
        }

        public FrostResistances ParseFrostResistances(string value)
        {
            switch (value)
            {
                case "none":
                    return FrostResistances.None;
                case "low":
                    return FrostResistances.Low;
                case "middle":
                    return FrostResistances.Middle;
                case "high":
                    return FrostResistances.High;
                case "complete":
                    return FrostResistances.Complete;
                default:
                    Console.WriteLine(
                        "Frost resistance {0} is not valid, to see valid frost resistances enter command: help filter values frostResistances",
                        value);
                    throw new ParsingException("Could not parse FrostResistances " + value);
            }
        }

        public DiseasesResistances ParseDesiasesResistances(string value)
        {
            switch (value)
            {
                case "none":
                    return DiseasesResistances.None;
                case "low":
                    return DiseasesResistances.Low;
                case "middle":
                    return DiseasesResistances.Middle;
                case "high":
                    return DiseasesResistances.High;
                case "complete":
                    return DiseasesResistances.Complete;
                default:
                    Console.WriteLine(
                        "Desiases resistances resistance {0} is not valid, to see valid desiases resistances enter command: help filter values desiasesResistances",
                        value);
                    throw new ParsingException("Could not parse DesiasesResistances " + value);
            }
        }

        public PestsResistances ParsePestsResistances(string value)
        {
            switch (value)
            {
                case "none":
                    return PestsResistances.None;
                    break;
                case "low":
                    return PestsResistances.Low;
                    break;
                case "middle":
                    return PestsResistances.Middle;
                    break;
                case "high":
                    return PestsResistances.High;
                    break;
                case "complete":
                    return PestsResistances.Complete;
                    break;
                default:
                    Console.WriteLine(
                        "Pests resistance {0} is not valid, to see valid pests resistances enter command: help filter values pestsResisnces",
                        value);
                    throw new ParsingException("Could not parse PestsResistances " + value);
            }
        }
    }
}