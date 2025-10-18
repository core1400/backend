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

        [HttpGet]
        public async Task<ActionResult<List<GetSubmissionRO>>> GetSeveralSubmissions([FromQuery] SubmissionsFilterDTO submissionsFilter)
        {
            return await _submissionsService.GetSeveralSubmissions(submissionsFilter);
        }

        [HttpPost]
        [RequireRole(UserRole.Student)]
        public async Task<ActionResult<CreateSubmissionRO>> CreateSubmission([FromBody] CreateSubmissionDTO createSubmissionDTO)
        {
            UserRole? role = HttpContext.Items[Consts.HTTP_CONTEXT_USER_ROLE] as UserRole?;
            if (role == null)
                return Forbid();
            return await _submissionsService.CreateSubmission(createSubmissionDTO);
        }

        [HttpGet("{submissionID}")]
        public async Task<ActionResult<GetSubmissionRO>> GetSpecificSubmission(string submissionID)
        {
            return await _submissionsService.GetSpecificSubmission(submissionID);
        }

        [HttpPatch("{submissionID}")]
        [Consumes("application/json")]
        [RequireRole(UserRole.Admin, UserRole.Commander, UserRole.Mamak, UserRole.Student)]
        public async Task<ActionResult> UpdateSpecificSubmission(string submissionID, [FromBody] JsonElement updateElement)
        {
            UserRole? role = HttpContext.Items[Consts.HTTP_CONTEXT_USER_ROLE] as UserRole?;
            if (role == null)
                return Forbid();
            return await _submissionsService.UpdateSpecificSubmission(submissionID, updateElement);
        }

        [HttpDelete("{submissionID}")]
        [RequireRole(UserRole.Admin, UserRole.Commander, UserRole.Mamak, UserRole.Student)]
        public async Task<ActionResult> RemoveSpecificSubmission(string submissionID)
        {
            UserRole? role = HttpContext.Items[Consts.HTTP_CONTEXT_USER_ROLE] as UserRole?;
            if (role == null)
                return Forbid();
            return await _submissionsService.RemoveSpecificSubmission(submissionID);
        }

        [HttpGet("{submissionID}/is-approved")]
        public async Task<ActionResult<IsSubmissionApprovedRO>> IsSubmissionApproved(string submissionID)
        {
            return await _submissionsService.IsSubmissionApproved(submissionID);
        }
    }
}