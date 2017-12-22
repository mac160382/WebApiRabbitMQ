// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskAggregateRepository.cs" company="GBM"> GBM GRUPO BURSÁTIL MEXICANO, S.A. DE C.V. CASA DE BOLSA. ©2017</copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using CallCenterModel;

using MongoDB.Driver;

namespace Repository
{
    public class TaskAggregateRepository : ITaskAggregateRepository
    {
        private readonly IMongoClient mongoClient;

        private const string DataBase = "calltask";

        private const string Collecton = "tasksAggregate";

        public TaskAggregateRepository()
        {
            mongoClient = new MongoDB.Driver.MongoClient("mongodb://localhost:27017");
        }

        public void Add(TaskAggregate taskAggregate)
        {
            var bd = mongoClient.GetDatabase(DataBase);
            var collections = bd.GetCollection<TaskAggregate>(Collecton);
            
            collections.InsertOne(taskAggregate);
        }

        public TaskAggregate Update(TaskAggregate taskAggregate)
        {
            var bd = mongoClient.GetDatabase(DataBase);
            var collections = bd.GetCollection<TaskAggregate>(Collecton);
            return collections.FindOneAndUpdate(x => x.Interval == taskAggregate.Interval,
                                                Builders<TaskAggregate>.Update
                                                                       .Set(nameof(TaskAggregate.Quantity), taskAggregate.Quantity)
                                                                       .Set(nameof(TaskAggregate.Resolved), taskAggregate.Resolved)
                                                                       .Set(nameof(TaskAggregate.Erlang), taskAggregate.Erlang)
                                                                       .Set(nameof(TaskAggregate.LevelService), taskAggregate.LevelService)
                                                                       );
        }

        public TaskAggregate Get(TimeSpan intervalTime)
        {
            var bd = mongoClient.GetDatabase(DataBase);
            var collections = bd.GetCollection<TaskAggregate>(Collecton);
            return collections.Find(x => x.Interval == intervalTime).FirstOrDefault();
        }
    }
}