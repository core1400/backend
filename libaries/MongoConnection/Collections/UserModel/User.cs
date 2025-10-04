using MongoConnection.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnection.Collections.UserModel
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("personalNumber")]
        public int PersonalNumber { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; } = null!;

        [BsonElement("lastName")]
        public string LastName { get; set; } = null!;

        [BsonElement("birthDate")]
        public DateTime BirthDate { get; set; }

        [BsonElement("testScores")]
        public Dictionary<string, int>? TestScores { get; set; } // Key: Test name, Value: Score

        [BsonElement("averageScore")]
        public float? AverageScore { get; set; }

        [BsonElement("misbehaviorCount")]
        public int? MisbehaviorCount { get; set; }

        [BsonElement("courseNumber")]
        public int? CourseNumber { get; set; }

        [BsonElement("role")]
        public UserRole Role { get; set; } // ADMIN, STUDENT, MAMAK, COMMANDER

        [BsonElement("isFirstConnection")]
        public bool IsFirstConnection { get; set; }
    }
}
