using CoreBackend.Features.Courses.DTOs;
using CoreBackend.Features.Courses.ROs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.Courses
{
    [ApiController]
    [Route("courses")]
    public class CoursesController : ControllerBase
    {

        public CoursesController()
        {
            // Dependences Here
        }

        [HttpPost]
        public ActionResult<CreateCourseRO> CreateCourse([FromBody] CreateCourseDTO createCourseDTO)
        {
            // Code Here
        }

        [HttpGet]
        public ActionResult<List<GetCourseRO>> GetSeveralCourses([FromQuery] CoursesFilterDTO coursesFilter)
        {
            // Code Here
        }

        [HttpGet("{courseID}")]
        public ActionResult<GetCourseRO> GetSpecificCourse(string courseID)
        {
            // Code Here
        }

        [HttpPatch("{courseID}")]
        public ActionResult UpdateSpecificCourse(string courseID)
        {
            // Code Here
        }

        [HttpDelete("{courseID}")]
        public ActionResult RemoveSpecificCourse([FromBody] string courseID)
        {
            // Code Here
        }

        [HttpPost("{courseID}/students")]
        public ActionResult AddUserToCourse([FromBody] AddUserToCourseDTO addUserToCourseDTO)
        {
            // Code Here
        }

        [HttpDelete("{courseID}/students/{studentID}")]
        public ActionResult RemoveUserFromCourse(string courseID, string studentID)
        {
            // Code Here
        }
    }
}
