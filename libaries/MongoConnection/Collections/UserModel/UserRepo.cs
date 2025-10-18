using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections.UserModel
{
    public class UserRepo : Repository<User>,IUserRepo
    {
        public UserRepo(MongoContext mongoContext) : base(mongoContext,Consts.USER_DATABASE_NAME)
        {
        }

        public async Task DeleteByPNumAsync(string personalNumber)
        {
            await _collection.DeleteOneAsync(user => user.PersonalNumber == personalNumber);
        }

        public async Task<User?> GetByPNumAsync(string personalNumber)
        {
            return await _collection.Find(user => user.PersonalNumber == personalNumber).FirstOrDefaultAsync();
        }

        public async Task UpdateByPNumAsync(string personalNumber, JsonElement updateElements)
        {
            UpdateDefinition<User> combined = GlobalTools<User>.GenericUpdate(updateElements);
            try
            {
                await _collection.UpdateOneAsync(user => user.PersonalNumber == personalNumber, combined);
            }
            catch (Exception e) { }
        }
    }
}
