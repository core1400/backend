namespace CoreBackend.Features.Grades.ROs
{
    public class GetGradeForExamRO
    {
        public class GradeForExamModel
        {
            public required string studentName { get; set; }
            public required int grade;
        }
        public required List<Dictionary<string, GradeForExamModel>> grades;
    }
}