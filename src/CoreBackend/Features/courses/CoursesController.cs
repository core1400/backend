using CoreBackend.Features.Courses.DTOs;
using CoreBackend.Features.Courses.ROs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection.Enums;
using System.Text.Json;

namespace CoreBackend.Features.Courses
{
    [ApiController]
    [Route("courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        [RequireRole(UserRole.Admin, UserRole.Mamak)]

        public async Task<ActionResult<CreateCourseRO>> CreateCourse([FromBody] CreateCourseDTO dto)
        {
            return await _courseService.CreateCourse(dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCourseRO>>> GetSeveralCourses([FromQuery] CoursesFilterDTO filter)
        {
            return await _courseService.GetSeveralCourses(filter);
        }

        [HttpGet("{courseID}")] 
        public async Task<ActionResult<GetCourseRO>> GetSpecificCourse(string courseID)
        {
            Console.WriteLine(courseID);
            return await _courseService.GetSpecificCourse(courseID);
        }

        [HttpPatch("{courseID}")]
        [RequireRole(UserRole.Admin, UserRole.Mamak)]

        public async Task<ActionResult> UpdateSpecificCourse(string courseID, [FromBody] JsonElement updateElements)
        {
            return await _courseService.UpdateSpecificCourse(courseID, updateElements);
        }

        [HttpDelete("{courseID}")]
        [RequireRole(UserRole.Admin, UserRole.Mamak)]

        public async Task<ActionResult> RemoveSpecificCourse(string courseID)
        {
            return await _courseService.RemoveSpecificCourse(courseID);
        }

        [HttpPost("{courseID}/students")]
        [RequireRole(UserRole.Admin, UserRole.Commander, UserRole.Mamak)]

        public async Task<ActionResult> AddUserToCourse(string courseID, [FromBody] AddUserToCourseDTO dto)
        {
            return await _courseService.AddUserToCourse(courseID, dto);
        }

        [HttpDelete("{courseID}/students/{studentID}")]
        [RequireRole(UserRole.Admin, UserRole.Commander, UserRole.Mamak)]

        public async Task<ActionResult> RemoveUserFromCourse(string courseID, string studentID)
        {
            return await _courseService.RemoveUserFromCourse(courseID, studentID);
        }
    }
}
