using CoreBackend.Features.Exams.DTOs;
using CoreBackend.Features.Exams.ROs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.Exams
{
    [ApiController]
    [Route("exams")]
    public class GradesController : ControllerBase
    {

        public GradesController()
        {
            // Dependences Here
        }

        [HttpPost("~/courses/{courseID}/exams")]
        public ActionResult<CreateExamRO> AddExamForCourse([FromBody] CreateExamDTO createExamDTO)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpGet("~/courses/{courseID}/exams")]
        public ActionResult<List<GetExamRO>> GetAllExamsForCourse(string courseID)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult<List<GetExamRO>> GetAllExams([FromQuery] GetAllExamsFilter getAllExamsFilter)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult CreateNewExam()
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpPatch]
        public ActionResult UpdateExam()
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpGet("~/courses/{examId}")]
        public ActionResult GetSpecificExam()
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpPatch("~/courses/{courseID}/exams")]
        public ActionResult UpdateSpecificExam()
        {
            // Code Here
            throw new NotImplementedException();
        }
        
    }
}
