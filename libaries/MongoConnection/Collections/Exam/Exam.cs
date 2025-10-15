using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnection.Collections.Exam
{
    public class Exam : BaseCollection
    {
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("type")]
        public string Type { get; set; } = null!;

        [BsonElement("minGrade")]
        public string MinGrade { get; set; } = null!;
    }
}
