using CoreBackend.Features.Users.DTOs;
using CoreBackend.Features.Users.ROs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection;
using MongoConnection.Collections.Course;
using MongoConnection.Collections.UserModel;
using MongoConnection.Enums;
using MongoDB.Driver;
using System.Data;
using System.Text.Json;

namespace CoreBackend.Features.users
{
    public class UserService : IUserService
    {
        private UserRepo _userRepo;
        private CourseRepo _courseRepo;
        public UserService(MongoContext mongoContext)
        {
            _userRepo = new UserRepo(mongoContext);
            _courseRepo = new CourseRepo(mongoContext);
        }
        public async Task<ActionResult<CreateUserRO>> CreateUser(CreateUserDTO createUserDTO,UserRole role)
        {
            if (role < createUserDTO.role)
                return new ForbidResult();

            MongoConnection.Collections.UserModel.User? isUserExist =  await _userRepo.GetByPNumAsync(createUserDTO.personalNum);
            if (isUserExist == null)
            {
                MongoConnection.Collections.UserModel.User newUser = new MongoConnection.Collections.UserModel.User
                {
                    PersonalNumber = createUserDTO.personalNum,
                    Password = createUserDTO.Password,
                    FirstName = createUserDTO.firstName,
                    LastName = createUserDTO.lastName,
                    BirthDate = createUserDTO.birthDate,
                    TestScores = new Dictionary<string, int>(),
                    AverageScore = 0,
                    MisbehaviorCount = 0,
                    CourseNumber = createUserDTO.courseNum,
                    Role = UserRole.Student,
                    IsFirstConnection = true
                };
                
                await _userRepo.CreateAsync(newUser);
                return new CreateUserRO() { user = newUser };
            }
            return new CreateUserRO() {  };
        }

        public async Task<ActionResult<List<GetUser>>> GetSeveralUsers(UsersFilterDTO usersFilter)
        {
            List<MongoConnection.Collections.UserModel.User> users = new List<MongoConnection.Collections.UserModel.User>();
            if (usersFilter.course!=null)
            {
                var filter = Builders<MongoConnection.Collections.UserModel.User>.Filter.Eq<string>(user=> user.CourseNumber, usersFilter.course);
                users =  (await _userRepo.GetByFilterAsync(filter)).ToList();
            }
            if(usersFilter.commander !=null)
            {
                List<Course> allCourses = new List<Course>();
                allCourses = (await _courseRepo.GetAllAsync()).ToList();
                foreach(var course in allCourses)
                {
                    if(course.Commanders.Contains(usersFilter.commander))
                    {
                        foreach(var userID in course.Students)
                        {
                            MongoConnection.Collections.UserModel.User? user = await _userRepo.GetByIdAsync(userID);
                            if(user != null && !users.Any(u=>u.Id == user.Id))
                                users.Add(user);
                        }
                    }
                }
            }
            if (usersFilter.mamak != null)
            {
                List<Course> allCourses = new List<Course>();
                allCourses = (await _courseRepo.GetAllAsync()).ToList();
                foreach (var course in allCourses)
                {
                    if (course.MamakId == usersFilter.mamak)
                    {
                        foreach (var userID in course.Students)
                        {
                            MongoConnection.Collections.UserModel.User? user = await _userRepo.GetByIdAsync(userID);
                            if (user != null && !users.Any(u => u.Id == user.Id))
                                users.Add(user);
                        }
                    }
                }
            }
            List<GetUser> getUsers = new List<GetUser>();
            foreach(MongoConnection.Collections.UserModel.User user in users)
            {
                GetUser getUser = new GetUser();
                getUser.User = user;
                getUsers.Add(getUser);
            }
            return getUsers;
        }

        public async Task<ActionResult<GetUser>> GetSpecificUser(string userID)
        {
            GetUser getUser = new GetUser();
            getUser.User = await _userRepo.GetByIdAsync(userID);
            return getUser;
        }

        public async Task<ActionResult> RemoveSpecificUser(string userID,UserRole role)
        {
            MongoConnection.Collections.UserModel.User? user = await _userRepo.GetByIdAsync(userID);
            if (user == null)
                return new NotFoundResult();
            UserRole toEditRole = user.Role;
            if (role > toEditRole)
                await _userRepo.DeleteByIdAsync(userID);
            return new ForbidResult();
        }

        public async Task<ActionResult> UpdateSpecificUser(string userID, JsonElement updateElements,UserRole role)
        {
            MongoConnection.Collections.UserModel.User? user = await _userRepo.GetByIdAsync(userID);
            if(user == null)
                return new NotFoundResult();
            UserRole toEditRole = user.Role;
            if(role > toEditRole)
                await _userRepo.UpdateAsync(userID, updateElements);
            return new ForbidResult();
        }
    }
}
