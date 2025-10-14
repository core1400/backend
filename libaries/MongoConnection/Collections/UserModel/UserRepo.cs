using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections.UserModel
{
    public class UserRepo : Repository<User>,IUserRepo
    {
        public IMongoCollection<User> _userCollection;
        public UserRepo(MongoContext mongoContext) : base(mongoContext)
        {
            _userCollection = mongoContext.GetCollection<User>(Consts.USER_DATABASE_NAME);    
        }

        public async Task DeleteByPNumAsync(int personalNumber)
        {
            await _userCollection.DeleteOneAsync(user => user.PersonalNumber == personalNumber);
        }

        public async Task<User?> GetByPNumAsync(int personalNumber)
        {
            return await _userCollection.Find(user => user.PersonalNumber == personalNumber).FirstOrDefaultAsync();
        }

        public async Task UpdateByPNumAsync(int personalNumber, JsonElement updateElements)
        {
            UpdateDefinition<User> combined = GlobalTools<User>.GenericUpdate(updateElements);
            try
            {
                await _userCollection.UpdateOneAsync(user => user.PersonalNumber == personalNumber, combined);
            }
            catch (Exception e) { }
        }
    }
}
