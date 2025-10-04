using MongoDB.Driver;

namespace MongoConnection.Collections.User
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

        public async Task DeleteByPersonalNumberAsync(int personalNumber)
        {
            await _userCollection.DeleteOneAsync(user => user.personalNumber == personalNumber);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userCollection.Find(FilterDefinition<User>.Empty).ToListAsync();
        }

        public async Task<User> GetByPersonalNumberAsync(int personalNumber)
        {
            return await _userCollection.Find(user => user.personalNumber == personalNumber).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, User user)
        {
            throw new NotImplementedException(); // To Do: think about  List<UpdateDefinition<User>> and partial user
            // how would it work with api and how to implement

        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _userCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            await _userCollection.DeleteOneAsync(user => user.Id == id);
        }
    }
}
