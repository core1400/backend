using CoreBackend.Features.Courses.DTOs;
using CoreBackend.Features.Courses.ROs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection;
using MongoConnection.Collections.Course;
using MongoConnection.Collections.UserModel;
using MongoDB.Driver;
using System.Text.Json;

namespace CoreBackend.Features.Courses
{
    public class CourseService : ICourseService
    {
        private readonly CourseRepo _courseRepo;

        public CourseService(MongoContext mongoContext)
        {
            _courseRepo = new CourseRepo(mongoContext);
        }

        public async Task<ActionResult<CreateCourseRO>> CreateCourse(CreateCourseDTO createCourseDTO)
        {
            Course? existing = await _courseRepo.GetByCNumAsync(createCourseDTO.courseNum);
            if (existing != null)
                return new ConflictResult(); // course already exists

            Course? course = new MongoConnection.Collections.Course.Course
            {
                MamakId = createCourseDTO.mamakId,
                Commanders = createCourseDTO.commanders.ToArray(),
                CourseNumber = createCourseDTO.courseNum,
                Name = createCourseDTO.name,
                Students = createCourseDTO.students.ToArray(),
                Department = createCourseDTO.department,
                ClassRepId = createCourseDTO.classRepId,
                HantarId = createCourseDTO.hantarId,
                Exams = Array.Empty<string>()
            };

            await _courseRepo.CreateAsync(course);
            return new CreateCourseRO { Course = course };
        }

        public async Task<ActionResult<List<GetCourseRO>>> GetSeveralCourses(CoursesFilterDTO coursesFilter)
        {
            FilterDefinitionBuilder<MongoConnection.Collections.Course.Course> builder =
                Builders<MongoConnection.Collections.Course.Course>.Filter;

            List<FilterDefinition<MongoConnection.Collections.Course.Course>> filters =
                new List<FilterDefinition<MongoConnection.Collections.Course.Course>>();

            if (!string.IsNullOrEmpty(coursesFilter.mamakID))
                filters.Add(builder.Eq(c => c.MamakId, coursesFilter.mamakID));

            FilterDefinition<MongoConnection.Collections.Course.Course> finalFilter =
                filters.Any() ? builder.And(filters) : builder.Empty;

            List<MongoConnection.Collections.Course.Course> courses =
                (await _courseRepo.GetByFilterAsync(finalFilter)).ToList();

            List<GetCourseRO> result =
                courses.Select(c => new GetCourseRO { Course = c }).ToList();

            return result;
        }

        public async Task<ActionResult<GetCourseRO>> GetSpecificCourse(string courseID)
        {
            Course? course = await _courseRepo.GetByIdAsync(courseID);
            if (course == null)
                return new NotFoundResult();

            return new GetCourseRO { Course = course };
        }

        public async Task<ActionResult> UpdateSpecificCourse(string courseID, JsonElement updateElements)
        {
            Course? course = await _courseRepo.GetByIdAsync(courseID);
            if (course == null)
                return new NotFoundResult();

            await _courseRepo.UpdateAsync(courseID, updateElements);
            return new NoContentResult();
        }

        public async Task<ActionResult> RemoveSpecificCourse(string courseID)
        {
            Course? course = await _courseRepo.GetByIdAsync(courseID);
            if (course == null)
                return new NotFoundResult();

            await _courseRepo.DeleteByIdAsync(courseID);
            return new NoContentResult();
        }

        public async Task<ActionResult> AddUserToCourse(string courseID, AddUserToCourseDTO dto)
        {
            Course? course = await _courseRepo.GetByIdAsync(courseID);
            if (course == null)
                return new NotFoundResult();

            List<string>? updatedStudents = (course.Students ?? Array.Empty<string>()).ToList();
            if (!updatedStudents.Contains(dto.userID))
                updatedStudents.Add(dto.userID);

            JsonElement json = JsonSerializer.SerializeToElement(new { Students = updatedStudents });
            await _courseRepo.UpdateByCNumAsync(course.CourseNumber, json);

            return new NoContentResult();
        }

        public async Task<ActionResult> RemoveUserFromCourse(string courseID, string studentID)
        {
            Course? course = await _courseRepo.GetByIdAsync(courseID);
            if (course == null)
                return new NotFoundResult();

            string[] updatedStudents = (course.Students ?? Array.Empty<string>()).Where(s => s != studentID).ToArray();
            JsonElement json = JsonSerializer.SerializeToElement(new { Students = updatedStudents });

            await _courseRepo.UpdateByCNumAsync(course.CourseNumber, json);
            return new NoContentResult();
        }
    }
}
