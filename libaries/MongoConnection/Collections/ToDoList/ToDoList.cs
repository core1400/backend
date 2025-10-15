using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnection.Collections.ToDoList
{
    public class ToDoList : BaseCollection
    {
        [BsonElement("personalNum")]
        public string PersonalNum { get; set; } = null!;

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("reminder")]
        public DateTime? Reminder { get; set; }

        [BsonElement("description")]
        public string Description { get; set; } = null!;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("completedAt")]
        public DateTime? CompletedAt { get; set; }
    }
}
