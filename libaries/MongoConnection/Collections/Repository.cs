using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections
{
    public class Repository<collectionType> : IRepository<collectionType> where collectionType : BaseCollection
    {
        public IMongoCollection<collectionType> _collection;
        public Repository(MongoContext mongoContext)
        {
            _collection = mongoContext.GetCollection<collectionType>(Consts.USER_DATABASE_NAME);
        }
        public async Task CreateAsync(collectionType entity)
        {
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
    }
}
