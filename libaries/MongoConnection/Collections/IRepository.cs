using System.Text.Json;

namespace MongoConnection.Collections
{
    internal interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task CreateAsync(T entity);
        Task UpdateAsync(string id, JsonElement updateElements);
        Task DeleteByIdAsync(string id);
    }
}
