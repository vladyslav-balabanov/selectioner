using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace BreadersHomebook.Models
{
    public class DatabaseManager
    {
        private readonly IMongoCollection<SelectionistsWorkModel> WorksTable;
        private const string WorksTableName = "selectionist-works";
        private const string DataBaseName = "selectionist";

        public DatabaseManager()
        {
            var database = new MongoClient().GetDatabase(DataBaseName);
            WorksTable = database.GetCollection<SelectionistsWorkModel>(WorksTableName);
        }
        
        public List<SelectionistsWorkModel> GetAllWorks()
        {
            return WorksTable.Find(_ => true).ToList();
        }

        public List<SelectionistsWorkModel> GetWorksByFilter(FilterModel filterModel)
        {
            var builder = Builders<SelectionistsWorkModel>.Filter.Where(work => true);
            
            builder = builder & Builders<SelectionistsWorkModel>.Filter.Where(work => work.Productivity >= filterModel.MinProductivity);
            builder = builder & Builders<SelectionistsWorkModel>.Filter.Where(work => work.Productivity <= filterModel.MaxProductivity);

            if (filterModel.VarietyName != null)
            {
                builder = builder & Builders<SelectionistsWorkModel>.Filter.Eq(work => work.NameOfCultureVariety, filterModel.VarietyName);
            }
            if (filterModel.Author != null)
            {
                builder = builder & Builders<SelectionistsWorkModel>.Filter.Eq(work => work.Author, filterModel.Author);
            }
            if (filterModel.ParentVarieties != null && filterModel.ParentVarieties.Count > 0)
            {
                builder = builder & Builders<SelectionistsWorkModel>.Filter.AnyIn(work => work.ParentVarieties, filterModel.ParentVarieties);
            }
            if (filterModel.FruitCharacteristics != null && filterModel.FruitCharacteristics.Count > 0)
            {
                builder = builder & Builders<SelectionistsWorkModel>.Filter.AnyIn(work => work.FruitCharacteristics, filterModel.FruitCharacteristics);
            }
            if (filterModel.FrostResistances != null && filterModel.FrostResistances.Count > 0)
            {
                builder = builder & Builders<SelectionistsWorkModel>.Filter.Where(work => filterModel.FrostResistances.Contains(work.FrostResistance));
            }
            if (filterModel.PestsResistances != null && filterModel.PestsResistances.Count > 0)
            {
                builder = builder & Builders<SelectionistsWorkModel>.Filter.Where(work => filterModel.PestsResistances.Contains(work.PestsResistance));
            }
            if (filterModel.DesiasesResistances != null && filterModel.DesiasesResistances.Count > 0)
            {
                builder = builder & Builders<SelectionistsWorkModel>.Filter.Where(work => filterModel.DesiasesResistances.Contains(work.DesiasesResistance));
            }
            if (filterModel.Fond != null)
            {
                builder = builder & Builders<SelectionistsWorkModel>.Filter.Where(work => work.FondsWithCulture.Contains(filterModel.Fond));
            }

            return WorksTable.Find(builder).ToList();
        }

        public SelectionistsWorkModel GetWorkById(int id)
        {
            return WorksTable.Find(work => work.Id == id).FirstOrDefault();
        }

        public void AddWork(SelectionistsWorkModel work)
        {
            work.Id = GenerateId();
            WorksTable.InsertOne(work);
        }

        public void UpdateWork(SelectionistsWorkModel work)
        {
            var filter = Builders<SelectionistsWorkModel>.Filter.Eq(b => b.Id, work.Id);
            WorksTable.ReplaceOne(filter, work);
        }

        public void DeleteWork(int id)
        {
            var filter = Builders<SelectionistsWorkModel>.Filter.Eq(bus => bus.Id, id);
            WorksTable.DeleteOne(filter);
        }

        private int GenerateId()
        {
            var works = WorksTable.Find(_ => true).ToList();
            if (works.Any())
            {
                return works.Max(bus => bus.Id) + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}