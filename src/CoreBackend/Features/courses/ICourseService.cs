using CoreBackend.Features.Courses.DTOs;
using CoreBackend.Features.Courses.ROs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CoreBackend.Features.Courses
{
    public interface ICourseService
    {
        Task<ActionResult<CreateCourseRO>> CreateCourse(CreateCourseDTO createCourseDTO);
        Task<ActionResult<List<GetCourseRO>>> GetSeveralCourses(CoursesFilterDTO coursesFilter);
        Task<ActionResult<GetCourseRO>> GetSpecificCourse(string courseID);
        Task<ActionResult> UpdateSpecificCourse(string courseID, JsonElement updateElements);
        Task<ActionResult> RemoveSpecificCourse(string courseID);
        Task<ActionResult> AddUserToCourse(string courseID, AddUserToCourseDTO addUserToCourseDTO);
        Task<ActionResult> RemoveUserFromCourse(string courseID, string userID);
    }
}
