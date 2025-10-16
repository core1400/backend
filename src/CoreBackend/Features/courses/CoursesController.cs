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
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult<List<GetCourseRO>> GetSeveralCourses([FromQuery] CoursesFilterDTO coursesFilter)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpGet("{courseID}")]
        public ActionResult<GetCourseRO> GetSpecificCourse(string courseID)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpPatch("{courseID}")]
        public ActionResult UpdateSpecificCourse(string courseID)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpDelete("{courseID}")]
        public ActionResult RemoveSpecificCourse([FromBody] string courseID)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpPost("{courseID}/students")]
        public ActionResult AddUserToCourse([FromBody] AddUserToCourseDTO addUserToCourseDTO)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpDelete("{courseID}/students/{studentID}")]
        public ActionResult RemoveUserFromCourse(string courseID, string studentID)
        {
            // Code Here
            throw new NotImplementedException();
        }
    }
}
