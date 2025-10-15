using System.Text.Json;

namespace MongoConnection.Collections.CalendarItem
{
    internal interface ICalendarItemRepo : IRepository<CalendarItem>
    {
        Task<CalendarItem?> GetByPNumAsync(string personalNubmer);
        Task DeleteByPNumAsync(string personalNubmer);
        Task UpdateByPNumAsync(string personalNumber, JsonElement updateElements);
    }
}
