using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections.Request
{
    public class RequestRepo : Repository<Request>, IRequestRepo
    {
        public RequestRepo(MongoContext mongoContext) : base(mongoContext, Consts.REQUEST_DATABASE_NAME)
        {
        }

        public async Task DeleteByPNumAsync(string personalNumber)
        {
            await _collection.DeleteOneAsync(request => request.PersonalNum == personalNumber);
        }

        public async Task<Request?> GetByPNumAsync(string personalNumber)
        {
            return await _collection.Find(request => request.PersonalNum == personalNumber).FirstOrDefaultAsync();

        }

        public async Task UpdateByPNumAsync(string personalNumber, JsonElement updateElements)
        {
            UpdateDefinition<Request> combined = GlobalTools<Request>.GenericUpdate(updateElements);
            try
            {
                await base._collection.UpdateOneAsync(request => request.PersonalNum == personalNumber, combined);
            }
            catch (Exception e) { }
        }
    }
}
