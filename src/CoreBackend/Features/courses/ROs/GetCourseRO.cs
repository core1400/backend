using MongoConnection.Collections.Course;

namespace CoreBackend.Features.Courses.ROs
{
    public class GetCourseRO
    {
        public required Course Course { get; set; }
    }
}