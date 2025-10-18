using System.Text.Json;
using CoreBackend.Features.Submissions.DTOs;
using CoreBackend.Features.Submissions.ROs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.Submissions
{
    public interface ISubmissionsService
    {
        // public Task<ActionResult<type[]>> GetSeveralSubmissions([FromBody] CreateSubmissionDTO createSubmissionDTO);
        public Task<ActionResult<CreateSubmissionRO>> CreateSubmission(CreateSubmissionDTO createSubmissionDTO);
        public Task<ActionResult<GetSubmissionRO>> GetSpecificSubmission(string submissionID);
        public Task<ActionResult> UpdateSpecificSubmission(string submissionID, JsonElement updateElement);
        public Task<ActionResult> RemoveSpecificSubmission(string submissionID);
        public Task<ActionResult<IsSubmissionApprovedRO>> IsSubmissionApproved(string submissionID);
    }
}