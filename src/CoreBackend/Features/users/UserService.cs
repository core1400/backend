using CoreBackend.Features.Users.DTOs;
using CoreBackend.Features.Users.ROs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection;
using MongoConnection.Collections.UserModel;
using MongoConnection.Enums;
using MongoDB.Driver;
using System.Text.Json;

namespace CoreBackend.Features.users
{
    public class UserService : IUserService
    {
        private UserRepo _userRepo;
        public UserService(MongoContext mongoContext)
        {
            _userRepo = new UserRepo(mongoContext);
        }
        public async Task<ActionResult<CreateUserRO>> CreateUser(CreateUserDTO createUserDTO)
        {
            User? isUserExist =  await _userRepo.GetByPNumAsync(createUserDTO.personalNum);
            if (isUserExist == null)
            {
                User newUser = new User
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
            }
            return new CreateUserRO();
        }

        public async Task<ActionResult<List<GetUser>>> GetSeveralUsers(UsersFilterDTO usersFilter)
        {
            FilterDefinitionBuilder<User> builder = Builders<User>.Filter;
            List<FilterDefinition<User>> filters = new List<FilterDefinition<User>>();

            if(usersFilter.course != null)
                filters.Add(builder.Eq(user => user.CourseNumber, usersFilter.course));
            //if(usersFilter.commander!= null)
                //filters.Add(builder.Eq(user => user.com, usersFilter.commander));

            var finalFilters = filters.Any()?builder.And(filters):builder.Empty;

            List<User> users =  (await _userRepo.GetByFilterAsync(finalFilters)).ToList();
            List<GetUser> getUsers = new List<GetUser>();
            foreach( User user in users)
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
            User? user = await _userRepo.GetByIdAsync(userID);
            if (user == null)
                return new NotFoundResult();
            UserRole toEditRole = user.Role;
            if (role > toEditRole)
                await _userRepo.DeleteByIdAsync(userID);
            return new NoContentResult();
        }

        public async Task<ActionResult> UpdateSpecificUser(string userID, JsonElement updateElements,UserRole role)
        {
            User? user = await _userRepo.GetByIdAsync(userID);
            if(user == null)
                return new NotFoundResult();
            UserRole toEditRole = user.Role;
            if(role > toEditRole)
                await _userRepo.UpdateAsync(userID, updateElements);
            return new NoContentResult();
        }
    }
}
