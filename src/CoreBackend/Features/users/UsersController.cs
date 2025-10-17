using CoreBackend.Features.auth;
using CoreBackend.Features.users;
using CoreBackend.Features.Users.DTOs;
using CoreBackend.Features.Users.ROs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection.Enums;
using System.Text.Json;

namespace CoreBackend.Features.Users
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private AuthFilter _authFilter;

        public UsersController(IUserService userService,AuthFilter authFilter)
        {
            _userService = userService;
            _authFilter = authFilter;
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
        public async Task<ActionResult> UpdateSpecificUser(string userID, [FromBody] JsonElement updateElement)
        {
            return await _userService.UpdateSpecificUser(userID, updateElement);
        }

        [HttpDelete("{userID}")]
        public async Task<ActionResult> RemoveSpecificUser(string userID)
        {
            return await _userService.RemoveSpecificUser(userID);
        }
    }
}