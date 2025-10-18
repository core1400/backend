using MongoConnection.Enums;

namespace CoreBackend.Features.Submissions.DTOs
{
    public class SubmissionsFilterDTO
    {
        public string? personalNum { get; set; }
        public SubmissionType? submissionType { get; set; }
        public string? requiredApprovedBy { get; set; }
        public string? approvedBy { get; set; }
    }
}