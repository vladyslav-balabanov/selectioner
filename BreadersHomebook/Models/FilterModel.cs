using System.Collections.Generic;
using System.Linq;

namespace BreadersHomebook.Models
{
    public class FilterModel
    {
        public string VarietyName { get; set; }
        public string Author { get; set; }
        public List<string> ParentVarieties { get; set; }
        public decimal MinProductivity { get; set; }
        public decimal MaxProductivity { get; set; }
        public List<FruitCharacteristics> FruitCharacteristics { get; set; }
        public List<FrostResistances> FrostResistances { get; set; }
        public List<PestsResistances> PestsResistances { get; set; }
        public List<DiseasesResistances> DiseasesResistances { get; set; }
        public string Fond { get; set; }

        public FilterModel()
        {
            FrostResistances = new List<FrostResistances>();
            FruitCharacteristics = new List<FruitCharacteristics>();
            PestsResistances = new List<PestsResistances>();
            DiseasesResistances = new List<DiseasesResistances>();
            ParentVarieties = new List<string>();
            MinProductivity = 0;
            MaxProductivity = decimal.MaxValue;
        }

        public override string ToString()
        {
            return string.Format("Filters: variety: {0}; author:{1}; parents:{2}; minProductivity:{3}; " +
                                 "maxProductivity:{4}; fruitCharacteristics:{5}; frostResistances:{6}; " +
                                 "pestsResistances:{7}; diseasesResistances:{8}; fond:{9}",
                VarietyName,
                Author,
                string.Join(",", ParentVarieties),
                MinProductivity,
                MaxProductivity,
                string.Join(",", FruitCharacteristics),
                string.Join(",", FrostResistances),
                string.Join(",", PestsResistances),
                string.Join(",", DiseasesResistances),
                Fond
            );
        }
    }
}