using MongoConnection.Collections.UserModel;

namespace MongoConnection.Collections.CalendarItem
{
    public class CalendarItemRepo : Repository<CalendarItem>, ICalendarItemRepo
    {
        public CalendarItemRepo(MongoContext mongoContext) : base(mongoContext)
        {
            _collection = mongoContext.GetCollection<CalendarItem>(Consts.COURSE_DATABASE_NAME);
        }
    }
}
