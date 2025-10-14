namespace CoreBackend.Features.Courses.DTOs
{
    public class CreateCourseDTO
    {
        public required string mamakId { get; set; }
        public required List<string> commanders { get; set; }
        public required string courseNum { get; set; }
        public required string name { get; set; }

        // students => A list of ids
        public required List<string> students { get; set; }
        public required string department { get; set; }
        public required string classRepId { get; set; }
        public required string hantarId { get; set; }
    }
}