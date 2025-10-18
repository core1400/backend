using System.Text.Json;

namespace MongoConnection.Collections.UserModel
{
    internal interface IUserRepo : IRepository<User>
    {
        Task<User?> GetByPNumAsync(string personalNubmer); 
        Task DeleteByPNumAsync(string personalNubmer);
        Task UpdateByPNumAsync(string personalNumber, JsonElement updateElements);
    }
}
