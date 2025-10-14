using System.Text.Json;

namespace MongoConnection.Collections.UserModel
{
    internal interface IUserRepo : IRepository<User>
    {
        Task<User?> GetByPNumAsync(int personalNubmer); 
        Task DeleteByPNumAsync(int personalNubmer);
        Task UpdateByPNumAsync(int personalNumber, JsonElement updateElements);
    }
}
