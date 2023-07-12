using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace BreadersHomebook.Models
{
    public class DatabaseManager
    {
        private const string WorksTableName = "selectionist-works";
        private const string DataBaseName = "selectionist";
        private readonly IMongoCollection<VarietyInfoModel> _worksTable;

        public DatabaseManager()
        {
            var database = new MongoClient().GetDatabase(DataBaseName);
            _worksTable = database.GetCollection<VarietyInfoModel>(WorksTableName);
        }

        public List<VarietyInfoModel> GetAllWorks()
        {
            return _worksTable.Find(_ => true).ToList();
        }

        public List<VarietyInfoModel> GetWorksByFilter(FilterModel filterModel)
        {
            var builder = Builders<VarietyInfoModel>.Filter.Where(work => true);

            builder = builder &
                      Builders<VarietyInfoModel>.Filter.Where(work =>
                          work.Productivity >= filterModel.MinProductivity);
            builder = builder &
                      Builders<VarietyInfoModel>.Filter.Where(work =>
                          work.Productivity <= filterModel.MaxProductivity);

            if (filterModel.VarietyName != null)
                builder = builder &
                          Builders<VarietyInfoModel>.Filter.Eq(work => work.NameOfCultureVariety,
                              filterModel.VarietyName);
            if (filterModel.Author != null)
                builder = builder & Builders<VarietyInfoModel>.Filter.Eq(work => work.Author, filterModel.Author);
            if (filterModel.ParentVarieties != null && filterModel.ParentVarieties.Count > 0)
                builder = builder &
                          Builders<VarietyInfoModel>.Filter.AnyIn(work => work.ParentVarieties,
                              filterModel.ParentVarieties);
            if (filterModel.FruitCharacteristics != null && filterModel.FruitCharacteristics.Count > 0)
                builder = builder & Builders<VarietyInfoModel>.Filter.AnyIn(work => work.FruitCharacteristics,
                    filterModel.FruitCharacteristics);
            if (filterModel.FrostResistances != null && filterModel.FrostResistances.Count > 0)
                builder = builder & Builders<VarietyInfoModel>.Filter.Where(work =>
                    filterModel.FrostResistances.Contains(work.FrostResistance));
            if (filterModel.PestsResistances != null && filterModel.PestsResistances.Count > 0)
                builder = builder & Builders<VarietyInfoModel>.Filter.Where(work =>
                    filterModel.PestsResistances.Contains(work.PestsResistance));
            if (filterModel.DiseaseResistances != null && filterModel.DiseaseResistances.Count > 0)
                builder = builder & Builders<VarietyInfoModel>.Filter.Where(work =>
                    filterModel.DiseaseResistances.Contains(work.DiseaseResistance));
            if (filterModel.Fond != null)
                builder = builder &
                          Builders<VarietyInfoModel>.Filter.Where(work =>
                              work.FondsWithCulture.Contains(filterModel.Fond));

            return _worksTable.Find(builder).ToList();
        }

        public VarietyInfoModel GetWorkById(int id)
        {
            return _worksTable.Find(work => work.Id == id).FirstOrDefault();
        }

        public void AddWork(VarietyInfoModel work)
        {
            work.Id = GenerateId();
            _worksTable.InsertOne(work);
        }

        public void UpdateWork(VarietyInfoModel work)
        {
            var filter = Builders<VarietyInfoModel>.Filter.Eq(b => b.Id, work.Id);
            _worksTable.ReplaceOne(filter, work);
        }

        public void DeleteWork(int id)
        {
            var filter = Builders<VarietyInfoModel>.Filter.Eq(bus => bus.Id, id);
            _worksTable.DeleteOne(filter);
        }

        private int GenerateId()
        {
            var works = _worksTable.Find(_ => true).ToList();
            if (works.Any())
                return works.Max(bus => bus.Id) + 1;
            return 1;
        }
    }
}