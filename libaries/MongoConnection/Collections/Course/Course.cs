using MongoConnection.Collections.UserModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnection.Collections.Course
{
    public class Course : BaseCollection
    {
        [BsonElement("mamakId")]
        public string MamakId { get; set; } = null!;

        [BsonElement("commanders")]
        public string[] Commanders { get; set; } = null!; // list of all commander ids

        [BsonElement("courseNumber")]
        public int CourseNumber { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("students")]
        public string[] Students { get; set; } = null!; // list of all student ids

        [BsonElement("department")]
        public string Department { get; set; } = null!;

        [BsonElement("classRepId")]
        public string? ClassRepId { get; set; }

        [BsonElement("hantarId")]
        public string? HantarId { get; set; }
    }
}
