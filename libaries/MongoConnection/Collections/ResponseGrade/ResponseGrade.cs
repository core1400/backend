using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnection.Collections.ResponseGrade
{
    public class ResponseGrade : BaseCollection
    {
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("grade")]
        public double Grade { get; set; }
    }
}
