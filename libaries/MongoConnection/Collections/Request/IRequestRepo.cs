using System.Text.Json;

namespace MongoConnection.Collections.Request
{
    internal interface IRequestRepo : IRepository<Request>
    {
        Task<Request?> GetByPNumAsync(string personalNubmer);
        Task DeleteByPNumAsync(string personalNubmer);
        Task UpdateByPNumAsync(string personalNumber, JsonElement updateElements);
    }
}
