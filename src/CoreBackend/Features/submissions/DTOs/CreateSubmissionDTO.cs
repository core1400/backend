using MongoConnection.Enums;

namespace CoreBackend.Features.Submissions.DTOs
{
    public class CreateSubmissionDTO
    {
        public required string personalNum  {get; set; }
        public required SubmissionType submissionType {get; set; }
        public required string description {get; set; }
        public required string[] requiredApprovedBy {get; set; }
        public required string[] approvedBy {get; set; }
    }
}