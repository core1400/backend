using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections.UserModel
{
    public class UserRepository : IUserRepository
    {
        public IMongoCollection<User> _userCollection;
        public UserRepository(MongoContext mongoContext)
        {
            _userCollection = mongoContext.GetCollection<User>(Consts.USER_DATABASE_NAME);    
        }

        public async Task CreateAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
        }

        public async Task DeleteByPNumAsync(int personalNumber)
        {
            await _userCollection.DeleteOneAsync(user => user.PersonalNumber == personalNumber);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userCollection.Find(FilterDefinition<User>.Empty).ToListAsync();
        }

        public async Task<User?> GetByPNumAsync(int personalNumber)
        {
            return await _userCollection.Find(user => user.PersonalNumber == personalNumber).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, JsonElement updateElements)
        {
            UpdateDefinition<User> combined = GlobalTools<User>.GenericUpdate(updateElements);
            await _userCollection.UpdateOneAsync(user => user.Id == id, combined);
        }

        public async Task UpdateByPNumAsync(int personalNumber, JsonElement updateElements)
        {
            UpdateDefinition<User> combined = GlobalTools<User>.GenericUpdate(updateElements);
            await _userCollection.UpdateOneAsync(user => user.PersonalNumber == personalNumber, combined);
        }
        
        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            await _userCollection.DeleteOneAsync(user => user.Id == id);
        }
    }
}
