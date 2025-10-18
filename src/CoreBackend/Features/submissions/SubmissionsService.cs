using System.Net;
using System.Text.Json;
using CoreBackend.Features.Submissions.DTOs;
using CoreBackend.Features.Submissions.ROs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoConnection;
using MongoConnection.Collections.Request;

namespace CoreBackend.Features.Submissions
{
    public class SubmissionsService : ISubmissionsService
    {
        private RequestRepo _requestRepo;

        public SubmissionsService(MongoContext mongoContext)
        {
            _requestRepo = new RequestRepo(mongoContext);
        }

        public async Task<ActionResult<CreateSubmissionRO>> CreateSubmission(CreateSubmissionDTO createSubmissionDTO)
        {
            Request newSubmission = new Request
            {
                PersonalNum = createSubmissionDTO.personalNum,
                RequestType = createSubmissionDTO.submissionType,
                Description = createSubmissionDTO.description,
                RequiredApprovedBy = createSubmissionDTO.requiredApprovedBy,
                ApprovedBy = createSubmissionDTO.approvedBy
            };

            await _requestRepo.CreateAsync(newSubmission);
            return new CreateSubmissionRO { submission = newSubmission };
        }

        public async Task<ActionResult<GetSubmissionRO>> GetSpecificSubmission(string submissionID)
        {
            Request? wantedSubmission = await _requestRepo.GetByIdAsync(submissionID);
            if (wantedSubmission is null)
                return new NotFoundResult();
            GetSubmissionRO getSubmissionRO = new GetSubmissionRO { submission = wantedSubmission };
            return getSubmissionRO;
        }

        public async Task<ActionResult> UpdateSpecificSubmission(string submissionID, JsonElement updateElement)
        {
            Request? wantedSubmission = await _requestRepo.GetByIdAsync(submissionID);
            if (wantedSubmission is null)
                return new NotFoundResult();

            await _requestRepo.UpdateAsync(submissionID, updateElement);
            return new OkResult();
        }

        public async Task<ActionResult> RemoveSpecificSubmission(string submissionID)
        {
            Request? wantedSubmission = await _requestRepo.GetByIdAsync(submissionID);
            if (wantedSubmission is null)
                return new NotFoundResult();

            await _requestRepo.DeleteByIdAsync(submissionID);
            return new OkResult();
        }

        public async Task<ActionResult<IsSubmissionApprovedRO>> IsSubmissionApproved(string submissionID)
        {
            Request? wantedSubmission = await _requestRepo.GetByIdAsync(submissionID);
            if (wantedSubmission is null)
                return new NotFoundResult();

            HashSet<string> requiredApprovedBy = new HashSet<string>(wantedSubmission.RequiredApprovedBy);
            HashSet<string> approvedBy = new HashSet<string>(wantedSubmission.ApprovedBy);
            IsSubmissionApprovedRO isSubmissionApprovedRO = new IsSubmissionApprovedRO
            {
                isSubmissionApproved = requiredApprovedBy.SetEquals(approvedBy)
            };
            return isSubmissionApprovedRO;
        }
    }
}

