using System.Text.Json;

namespace MongoConnection.Collections.Course
{
    internal interface ICourseRepo : IRepository<Course>
    {
        Task<Course?> GetByCNumAsync(string personalNubmer);
        Task DeleteByCNumAsync(string personalNubmer);
        Task UpdateByCNumAsync(string personalNumber, JsonElement updateElements);
    }
}
