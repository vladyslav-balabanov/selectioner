using System.Collections.Generic;

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
        public List<DesiasesResistances> DesiasesResistances { get; set; }
        public string Fond { get; set; }
    }
}