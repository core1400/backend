using MongoConnection.Enums;

namespace MongoConnection.Collections.User
{
    public class User
    {
        public int personalNumber { get; set; } 
        public string firstName { get; set; }   
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public Dictionary<string, int> testScores {get;set; } // Key: Test name, Value: Score
        public float averageScore { get; set; }
        public int misbehaviorCount { get; set; }
        public int courseNumber{ get; set; }
        public UserRole role { get; set; } // ADMIN, STUDENT, MAMAK, COMMANDER 
        public bool isFirstConnection { get; set; } 
    }
}
