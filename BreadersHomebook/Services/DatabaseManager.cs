using System;
using System.Collections.Generic;
using System.Linq;
using BreadersHomebook.Models;
using MongoDB.Driver;

namespace BreadersHomebook.Services
{
    public class DatabaseManager
    {
        private const string WorksTableName = "selectionist-works";
        private const string DataBaseName = "selectionist";
        private const string User = "BreadersHomebookReader";
        private const string Password = "Ego7yQmBYuGimwFe";

        private const string ConnectionUri =
            "mongodb+srv://" + User + ":" + Password + "@courseworkcluster.mpwgv3z.mongodb.net/?retryWrites=true&w=majority";

        private readonly IMongoCollection<VarietyInfoModel> _worksTable;

        public DatabaseManager()
        {
            var settings = MongoClientSettings.FromConnectionString(ConnectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var database = new MongoClient(settings).GetDatabase(DataBaseName);
            _worksTable = database.GetCollection<VarietyInfoModel>(WorksTableName);
        }

        public List<VarietyInfoModel> GetAllWorks()
        {
            return _worksTable.Find(_ => true).ToList();
        }

        public List<VarietyInfoModel> GetWorksByFilter(FilterModel filterModel)
        {
            var builder = Builders<VarietyInfoModel>.Filter;
            var filter =
                builder.Where(work => work.Productivity >= filterModel.MinProductivity) & 
                builder.Where(work => work.Productivity <= filterModel.MaxProductivity);

            if (!string.IsNullOrEmpty(filterModel.VarietyName)) 
            {
                filter &= builder.Eq(work => work.NameOfCultureVariety,
                    filterModel.VarietyName);
            }
            if (!string.IsNullOrEmpty(filterModel.Author))
            {
                filter &= builder.Eq(work => work.Author, filterModel.Author);
            }

            if (filterModel.ParentVarieties != null && filterModel.ParentVarieties.Count > 0)
            {
                filter &= builder.All(work => work.ParentVarieties, filterModel.ParentVarieties);
            }

            if (filterModel.FruitCharacteristics != null && filterModel.FruitCharacteristics.Count > 0)
            {
                filter &= builder.All(work => work.FruitCharacteristics, filterModel.FruitCharacteristics);
            }

            if (filterModel.FrostResistances != null && filterModel.FrostResistances.Count > 0)
            {
                filter &= builder.In(work => work.FrostResistance, filterModel.FrostResistances);
            }

            if (filterModel.PestsResistances != null && filterModel.PestsResistances.Count > 0)
            {
                filter &= builder.In(work => work.PestsResistance, filterModel.PestsResistances);
            }

            if (filterModel.DiseaseResistances != null && filterModel.DiseaseResistances.Count > 0)
            {
                filter &= builder.In(work => work.DiseaseResistance, filterModel.DiseaseResistances);
            }

            if (!string.IsNullOrEmpty(filterModel.Fond))
            {
                filter &= builder.AnyEq(work => work.FondsWithCulture, filterModel.Fond);
            }

            Console.WriteLine(filterModel.ToString());

            return _worksTable.Find(filter).ToList();
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