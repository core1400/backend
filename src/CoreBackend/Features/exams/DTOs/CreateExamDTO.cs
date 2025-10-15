namespace CoreBackend.Features.Exams.DTOs
{
    public class CreateExamDTO
    {
        public required string name { get; set; }
        // public required ExamType type { get; set; }
        public required int minGrade { get; set; }
    }
}