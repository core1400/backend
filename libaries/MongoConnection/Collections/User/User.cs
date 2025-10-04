using MongoConnection.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnection.Collections.User
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public int personalNumber { get; set; } 
        public string firstName { get; set; }   
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public Dictionary<string, int> ?testScores {get;set; } // Key: Test name, Value: Score
        public float ?averageScore { get; set; }
        public int ?misbehaviorCount { get; set; }
        public int ?courseNumber{ get; set; }
        public UserRole role { get; set; } // ADMIN, STUDENT, MAMAK, COMMANDER 
        public bool isFirstConnection { get; set; } 
    }
}
