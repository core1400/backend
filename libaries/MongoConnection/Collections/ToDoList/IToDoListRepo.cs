using System.Text.Json;

namespace MongoConnection.Collections.ToDoList
{
    internal interface IToDoListRepo : IRepository<ToDoList>
    {
        Task<ToDoList?> GetByPNumAsync(string personalNubmer);
        Task DeleteByPNumAsync(string personalNubmer);
        Task UpdateByPNumAsync(string personalNumber, JsonElement updateElements);
    }
}
