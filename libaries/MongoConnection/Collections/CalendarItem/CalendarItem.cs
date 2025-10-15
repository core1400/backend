using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnection.Collections.CalendarItem
{
    public class CalendarItem : BaseCollection
    {
        [BsonElement("personalNum")]
        public string PersonalNum { get; set; } = null!;

        [BsonElement("startAt")]
        public DateTime StartAt { get; set; }

        [BsonElement("endAt")]
        public DateTime EndAt { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("description")]
        public string Description { get; set; } = null!;

        [BsonElement("backColor")]
        public string BackColor { get; set; } = null!;
    }
}
