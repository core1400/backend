using MongoConnection.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnection.Collections.Exam
{
    public class Exam : BaseCollection
    {
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("type")]
        public ExamType Type { get; set; }

        [BsonElement("minGrade")]
        public string MinGrade { get; set; } = null!;
    }
}
