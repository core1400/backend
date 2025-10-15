using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnection.Collections.Request
{
    public class Request : BaseCollection
    {
        [BsonElement("personalNum")]
        public string PersonalNum { get; set; } = null!;

        [BsonElement("requestType")]
        public string RequestType { get; set; } = null!;

        [BsonElement("description")]
        public string Description { get; set; } = null!;

        [BsonElement("requiredApprovedBy")]
        public string[] RequiredApprovedBy { get; set; } = null!;

        [BsonElement("approvedBy")]
        public string[] ApprovedBy { get; set; } = null!;
    }
}
