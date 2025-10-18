using System.Text.Json;
using CoreBackend.Features.Submissions.DTOs;
using CoreBackend.Features.Submissions.ROs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection.Enums;

namespace CoreBackend.Features.Submissions
{
    [ApiController]
    [Route("submissions")]
    public class SubmissionsController : ControllerBase
    {
        private ISubmissionsService _submissionsService;

        public SubmissionsController(ISubmissionsService submissionsService)
        {
            _submissionsService = submissionsService;
        }

        [HttpPost]
        // [RequireRole(UserRole.Student)]
        public async Task<ActionResult<CreateSubmissionRO>> CreateSubmission([FromBody] CreateSubmissionDTO createSubmissionDTO)
        {
            // UserRole? role = HttpContext.Items[Consts.HTTP_CONTEXT_USER_ROLE] as UserRole?;
            // if (role == null)
            //     return Forbid();
            Console.WriteLine(createSubmissionDTO.personalNum);
            return await _submissionsService.CreateSubmission(createSubmissionDTO);
        }

        [HttpGet("{submissionID}")]
        public async Task<ActionResult<GetSubmissionRO>> GetSpecificSubmission(string submissionID)
        {
            return await _submissionsService.GetSpecificSubmission(submissionID);
        }

        [HttpPatch("{submissionID}")]
        public async Task<ActionResult> UpdateSpecificSubmission(string submissionID, [FromBody] JsonElement updateElement)
        {
            return await _submissionsService.UpdateSpecificSubmission(submissionID, updateElement);
        }

        [HttpDelete("{submissionID}")]
        public async Task<ActionResult> RemoveSpecificSubmission(string submissionID)
        {
            return await _submissionsService.RemoveSpecificSubmission(submissionID);
        }

        [HttpGet("{submissionID}/is-approved")]
        public async Task<ActionResult<IsSubmissionApprovedRO>> IsSubmissionApproved(string submissionID)
        {
            return await _submissionsService.IsSubmissionApproved(submissionID);
        }

    }
}