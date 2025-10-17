using CoreBackend.Features.auth;
using CoreBackend.Features.users;
using CoreBackend.Features.Users.DTOs;
using CoreBackend.Features.Users.ROs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection.Enums;
using System.Security.Claims;
using System.Text.Json;

namespace CoreBackend.Features.Users
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserRO>> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            return await _userService.CreateUser(createUserDTO);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetUser>>> GetSeveralUsers([FromQuery] UsersFilterDTO usersFilter)
        {
            return await _userService.GetSeveralUsers(usersFilter);
        }

        [HttpGet("{userID}")]
        public async Task<ActionResult<GetUser>> GetSpecificUser(string userID)
        {
            return await _userService.GetSpecificUser(userID);
        }

        [HttpPatch("{userID}")]
        [Consumes("application/json")]
        [RequireRole(UserRole.Admin, UserRole.Commander, UserRole.Mamak)]

        public async Task<ActionResult> UpdateSpecificUser(string userID, [FromBody] JsonElement updateElement)
        {
            UserRole? role = HttpContext.Items[Consts.HTTP_CONTEXT_USER_ROLE] as UserRole?;
            if (role == null)
                return Forbid();

            return await _userService.UpdateSpecificUser(userID, updateElement,role??UserRole.Student);
        }

        [HttpDelete("{userID}")]
        [RequireRole(UserRole.Admin,UserRole.Commander,UserRole.Mamak)]
        public async Task<ActionResult> RemoveSpecificUser(string userID)
        {
            UserRole? role = HttpContext.Items[Consts.HTTP_CONTEXT_USER_ROLE] as UserRole?;
            if(role == null)
                return Forbid();
            
            return await _userService.RemoveSpecificUser(userID,role??UserRole.Student);
        }
    }
}