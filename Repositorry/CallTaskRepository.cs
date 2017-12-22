using CallCenterModel;

using MongoDB.Driver;

namespace Repository
{
    public class CallTaskRepository : ICallTaskRepository
    {
        private const string Collecton = "tasks";

        private const string DataBase = "calltask";
        private readonly IMongoClient mongoClient;

        public CallTaskRepository() { mongoClient = new MongoClient("mongodb://localhost:27017"); }

        public void Add(Task task)
        {
            var bd = mongoClient.GetDatabase(DataBase);
            var collections = bd.GetCollection<Task>(Collecton);
            task._id = task.CorrelationId;
            collections.InsertOne(task);
        }

        public Task Update(Task task)
        {
            var bd = mongoClient.GetDatabase(DataBase);
            var collections = bd.GetCollection<Task>(Collecton);
            return collections.FindOneAndUpdate(x => x.CorrelationId == task.CorrelationId,
                                                Builders<Task>.Update.Set(nameof(Task.EndTime), task.EndTime));
        }
    }
}