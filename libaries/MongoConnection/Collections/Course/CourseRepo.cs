namespace MongoConnection.Collections.Course
{
    public class CourseRepo : Repository<Course>,ICourseRepo
    {
        public CourseRepo(MongoContext mongoContext) : base(mongoContext)
        {
            _collection = mongoContext.GetCollection<Course>(Consts.COURSE_DATABASE_NAME);
        }
    }
}
