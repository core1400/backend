using MongoConnection.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnection.Collections.UserModel
{
    public class User : BaseCollection
    {
        [BsonElement("personalNumber")]
        public string PersonalNumber { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; } = null!;

        [BsonElement("password")]
        public string Password { get; set; } = null!;

        [BsonElement("lastName")]
        public string LastName { get; set; } = null!;

        [BsonElement("birthDate")]
        public DateOnly BirthDate { get; set; }

        [BsonElement("testScores")]
        public Dictionary<string, int>? TestScores { get; set; } // Key: Test name, Value: Score

        [BsonElement("averageScore")]
        public float? AverageScore { get; set; }

        [BsonElement("misbehaviorCount")]
        public int? MisbehaviorCount { get; set; }

        [BsonElement("courseNumber")]
        public string? CourseNumber { get; set; }

        [BsonElement("role")]
        public UserRole Role { get; set; } // ADMIN, STUDENT, MAMAK, COMMANDER

        [BsonElement("isFirstConnection")]
        public bool IsFirstConnection { get; set; }
    }
}
