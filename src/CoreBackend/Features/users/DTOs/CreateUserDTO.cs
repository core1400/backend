namespace CoreBackend.Features.Users.DTOs
{
    public class CreateUserDTO
    {
        public required string personalNum { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public DateOnly birthDate { get; set; }

        // Dict type => First item: ExamID name | Second item: Exam grade
        public required Dictionary<string, int> testScores { get; set; }
        public required int misbehaviorCount { get; set; }
        public required string courseNum { get; set; }
        // public required Role role { get; set; } // *** Add the role from the lib *** \\
    }
}