using System;
using System.Collections.Generic;

using CallCenterModel;

using MongoDB.Driver;

namespace Repository
{
    public class TaskAggregateRepository : ITaskAggregateRepository
    {
        private const string Collecton = "tasksAggregate";

        private const string DataBase = "calltask";
        private readonly IMongoClient mongoClient;

        public TaskAggregateRepository() { mongoClient = new MongoClient("mongodb://localhost:27017"); }

        public void Add(TaskAggregate taskAggregate)
        {
            var bd = mongoClient.GetDatabase(DataBase);
            var collections = bd.GetCollection<TaskAggregate>(Collecton);

            collections.InsertOne(taskAggregate);
        }

        public TaskAggregate Get(TimeSpan intervalTime)
        {
            var bd = mongoClient.GetDatabase(DataBase);
            var collections = bd.GetCollection<TaskAggregate>(Collecton);
            return collections.Find(x => x.Interval == intervalTime).FirstOrDefault();
        }

        public List<TaskAggregate> GetAll()
        {
            var bd = mongoClient.GetDatabase(DataBase);
            var collections = bd.GetCollection<TaskAggregate>(Collecton);
            return collections.AsQueryable().ToList();
        }

        public TaskAggregate Update(TaskAggregate taskAggregate)
        {
            var bd = mongoClient.GetDatabase(DataBase);
            var collections = bd.GetCollection<TaskAggregate>(Collecton);
            return collections.FindOneAndUpdate(x => x.Interval == taskAggregate.Interval,
                                                Builders<TaskAggregate>.Update
                                                                       .Set(nameof(TaskAggregate.Quantity),
                                                                            taskAggregate.Quantity)
                                                                       .Set(nameof(TaskAggregate.Resolved),
                                                                            taskAggregate.Resolved)
                                                                       .Set(nameof(TaskAggregate.Erlang),
                                                                            taskAggregate.Erlang)
                                                                       .Set(nameof(TaskAggregate.LevelService),
                                                                            taskAggregate.LevelService)
            );
        }
    }
}