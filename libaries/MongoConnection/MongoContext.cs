using MongoDB.Driver;

namespace MongoConnection
{
    public class MongoContext
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly MongoSettings _mongoSettings;
        public MongoContext(MongoSettings mongoSettings)
        {
            _mongoSettings = mongoSettings;
            _mongoClient = new MongoClient(_mongoSettings.ConnectionUrl);
            _database = _mongoClient.GetDatabase(_mongoSettings.DataBaseName);
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
