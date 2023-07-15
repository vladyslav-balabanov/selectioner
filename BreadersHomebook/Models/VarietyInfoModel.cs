using System;
using System.Collections.Generic;
using BreadersHomebook.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace BreadersHomebook.Models
{
    public class VarietyInfoModel
    {
        [BsonId]
        public int Id { get; set; }
        public string NameOfCultureVariety { get; set; }
        public string Author { get; set; }
        public List<string> ParentVarieties { get; set; }
        public int Productivity { get; set; }
        public List<FruitCharacteristics> FruitCharacteristics { get; set; }
        public FrostResistances FrostResistance { get; set; }
        public PestsResistances PestsResistance { get; set; }
        public DiseaseResistances DiseaseResistance { get; set; }
        public List<string> FondsWithCulture { get; set; }

        public void Print()
        {
            Console.WriteLine(
                "id:{0}; name:{1}; author:{2}; parents:{3}; productivity:{4}; fruitCharacteristics:{5}; frostResistance:{6}; pestsResistance:{7}; diseaseResistance:{8}; fonds:{9};",
                Id, NameOfCultureVariety, Author, string.Join(",", ParentVarieties), Productivity,
                string.Join(",", FruitCharacteristics), FrostResistance, PestsResistance, DiseaseResistance,
                string.Join(",", FondsWithCulture));
        }
    }
}