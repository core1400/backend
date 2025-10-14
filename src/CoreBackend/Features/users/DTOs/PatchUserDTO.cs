namespace CoreBackend.Features.Users.DTOs
{
    public class PatchUserDTO
    {
        public int personalNum { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public DateOnly? birthDate { get; set; }

        // Dict type => First item: Exam name | Second item: Exam grade
        public Dictionary<string, int>? testScores { get; set; }
        public int? misbehaviorCount { get; set; }
        public int? courseNum { get; set; }
        // public Role? role { get; set; } // *** Add the role from the lib *** \\
    }
}