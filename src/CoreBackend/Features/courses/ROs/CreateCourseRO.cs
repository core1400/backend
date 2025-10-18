using MongoConnection.Collections.Course;

namespace CoreBackend.Features.Courses.ROs
{
    public class CreateCourseRO
    {
        public required Course Course { get; set; }
    }
}