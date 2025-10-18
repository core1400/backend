using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections
{
    public class Repository<collectionType> : IRepository<collectionType> where collectionType : BaseCollection
    {
        public IMongoCollection<collectionType> _collection;
        public Repository(MongoContext mongoContext,string dataBaseName)
        {
            _collection = mongoContext.GetCollection<collectionType>(dataBaseName);
        }
        public async Task CreateAsync(collectionType entity)
        {
            Console.WriteLine( "inserting");
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteByIdAsync(string id)
        {
            await _collection.DeleteOneAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<collectionType>> GetAllAsync()
        {
            return await _collection.Find(FilterDefinition<collectionType>.Empty).ToListAsync();
        }

        public async Task<collectionType?> GetByIdAsync(string id)
        {
            return await _collection.Find(entity => entity.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, JsonElement updateElements)
        {
            UpdateDefinition<collectionType> combined = GlobalTools<collectionType>.GenericUpdate(updateElements);
            try
            {
                await _collection.UpdateOneAsync(entity => entity.Id == id, combined);
            }
            catch (Exception e) { }
        }
        public async Task<List<collectionType>> GetByFilterAsync(FilterDefinition<collectionType> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }
    }
}
