using CoreBackend.Features.Users.DTOs;
using CoreBackend.Features.Users.ROs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MongoConnection.Enums;

namespace CoreBackend.Features.users
{
    public interface IUserService
    {
        public Task<ActionResult<CreateUserRO>> CreateUser(CreateUserDTO createUserDTO,UserRole role);
        public Task<ActionResult<List<GetUser>>> GetSeveralUsers(UsersFilterDTO usersFilter);
        public Task<ActionResult<GetUser>> GetSpecificUser(string userID);
        public Task<ActionResult> UpdateSpecificUser(string userID, JsonElement updateElement, UserRole role);
        public Task<ActionResult> RemoveSpecificUser(string userID,UserRole role);
    }
}
