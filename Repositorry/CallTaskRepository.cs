
using CallCenterModel;
using MongoDB.Driver;

namespace Repository
{
    public class CallTaskRepository : ICallTaskRepository
    {
        private MongoDB.Driver.IMongoClient mongoClient;

        public CallTaskRepository()
        {
            mongoClient = new MongoDB.Driver.MongoClient("mongodb://localhost:27017");
        }

        public void Add(Task task)
        {
            var bd = mongoClient.GetDatabase("calltask");
            var collections = bd.GetCollection<Task>("tasks");
            task._id = task.CorrelationId;
            collections.InsertOne(task);
        }

        public Task Update(Task task)
        {
            var bd = mongoClient.GetDatabase("calltask");
            var collections = bd.GetCollection<Task>("tasks");
            return collections.FindOneAndUpdate(x => x.CorrelationId == task.CorrelationId, Builders<Task>.Update.Set(nameof(Task.EndTime), task.EndTime));
        }
    }
}
