using MongoConnection.Collections.Request;

namespace CoreBackend.Features.Submissions.ROs
{
    public class GetSubmissionRO
    {
        public Request? submission { get; set; }
    }
}