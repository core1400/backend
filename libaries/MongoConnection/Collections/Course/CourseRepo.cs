using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections.Course
{
    public class CourseRepo : Repository<Course>,ICourseRepo
    {
        public CourseRepo(MongoContext mongoContext) : base(mongoContext, Consts.COURSE_DATABASE_NAME)
        {
        }
        public async Task DeleteByCNumAsync(string CourseNumber)
        {
            await _collection.DeleteOneAsync(request => request.CourseNumber == CourseNumber);
        }

        public async Task<Course?> GetByCNumAsync(string CourseNumber)
        {
            return await _collection.Find(request => request.CourseNumber == CourseNumber).FirstOrDefaultAsync();

        }

        public async Task UpdateByCNumAsync(string CourseNumber, JsonElement updateElements)
        {
            UpdateDefinition<Course> combined = GlobalTools<Course>.GenericUpdate(updateElements);
            try
            {
                await base._collection.UpdateOneAsync(request => request.CourseNumber == CourseNumber, combined);
            }
            catch (Exception e) { }
        }
    }
}
