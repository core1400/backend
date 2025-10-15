using CoreBackend.Features.Grades.DTOs;
using CoreBackend.Features.Grades.ROs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.Grades
{
    [ApiController]
    [Route("grades")]
    public class GradesController : ControllerBase
    {

        public GradesController()
        {
            // Dependences Here
        }

        [HttpGet("~/users/{userID}/grades")]
        // Dict type => First item: Exam name | Second item: Exam grade
        public ActionResult<Dictionary<string, int>> GetAllTheGradesForSpecificUser()
        {
            // Code Here
        }

        [HttpGet("~/grades/{examID}")]
        public ActionResult<GetGradeForExamRO> GetAllTheGradesForSpecificExam()
        {
            // Code Here
        }

        [HttpDelete("~/grades/{examID}")]
        public ActionResult DeleteAllTheGradesForSpecificExam()
        {
            // Code Here
        }
    }
}
