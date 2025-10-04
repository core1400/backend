using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnection.Collections.User
{
    public class UserRepository : IUserRepository
    {
        public IMongoCollection<User> _userCollection;
        public UserRepository(MongoContext mongoContext)
        {
            _userCollection = mongoContext.GetCollection<User>("Users");    
        }

        public Task CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
