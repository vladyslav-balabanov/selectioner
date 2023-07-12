using System;
using BreadersHomebook.Models;

namespace BreadersHomebook.Services
{
    public class EnumParser
    {
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

        public DiseaseResistances ParseDiseaseResistances(string value)
        {
            switch (value)
            {
                case "none":
                    return DiseaseResistances.None;
                case "low":
                    return DiseaseResistances.Low;
                case "middle":
                    return DiseaseResistances.Middle;
                case "high":
                    return DiseaseResistances.High;
                case "complete":
                    return DiseaseResistances.Complete;
                default:
                    Console.WriteLine(
                        "Disease resistances resistance {0} is not valid, to see valid disease resistances enter command: help filter values diseaseResistances",
                        value);
                    throw new ParsingException("Could not parse DiseaseResistances " + value);
            }
        }

        public PestsResistances ParsePestsResistances(string value)
        {
            switch (value)
            {
                case "none":
                    return PestsResistances.None;
                case "low":
                    return PestsResistances.Low;
                case "middle":
                    return PestsResistances.Middle;
                case "high":
                    return PestsResistances.High;
                case "complete":
                    return PestsResistances.Complete;
                default:
                    Console.WriteLine(
                        "Pests resistance {0} is not valid, to see valid pests resistances enter command: help filter values pestsResistances",
                        value);
                    throw new ParsingException("Could not parse PestsResistances " + value);
            }
        }
    }
}