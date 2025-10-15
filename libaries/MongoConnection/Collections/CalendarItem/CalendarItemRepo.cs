using MongoConnection.Collections.UserModel;
using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections.CalendarItem
{
    public class CalendarItemRepo : Repository<CalendarItem>, ICalendarItemRepo
    {
        public CalendarItemRepo(MongoContext mongoContext) : base(mongoContext, Consts.CALENDAR_DATABASE_NAME)
        {
        }
        public async Task DeleteByPNumAsync(string personalNumber)
        {
            await _collection.DeleteOneAsync(calendarItem => calendarItem.PersonalNum == personalNumber);
        }

        public async Task<CalendarItem?> GetByPNumAsync(string personalNumber)
        {
            return await _collection.Find(calendarItem => calendarItem.PersonalNum == personalNumber).FirstOrDefaultAsync();
        }

        public async Task UpdateByPNumAsync(string personalNumber, JsonElement updateElements)
        {
            UpdateDefinition<CalendarItem> combined = GlobalTools<CalendarItem>.GenericUpdate(updateElements);
            try
            {
                await base._collection.UpdateOneAsync(calendarItem => calendarItem.PersonalNum == personalNumber, combined);
            }
            catch (Exception e) { }
        }
    }
}
