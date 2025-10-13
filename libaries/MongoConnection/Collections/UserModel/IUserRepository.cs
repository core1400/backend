using System.Text.Json;

namespace MongoConnection.Collections.UserModel
{
    internal interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByPersonalNumberAsync(int personalNubmer); 
        Task DeleteByPersonalNumberAsync(int personalNubmer);
        Task UpdateByPersonalNumberAsync(int personalNumber, JsonElement updateElements);

    }
}
