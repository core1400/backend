using CoreBackend.Features.auth;
using CoreBackend.Features.auth.DTOs;
using CoreBackend.Features.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection;
using MongoConnection.Collections.UserModel;
using MongoConnection.Enums;
using System.Data;
using System.Text.Json;

namespace CoreBackend.Features.Auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private UserRepo _userRepo;
        private JwtService _jwtService;
        public AuthController(MongoContext mongoContext, JwtService jwtService)
        {
            _userRepo = new UserRepo(mongoContext);
            _jwtService = jwtService;
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<bool>> TryAuthenticate([FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            var user = await _userRepo.GetByPNumAsync(userCredentialsDTO.personalNumber);

            if (user == null)
                return Unauthorized();
            
            if (user.Password != userCredentialsDTO.password)
                return Unauthorized();

            var token = _jwtService.GenerateToken(user.Id,user.Role.ToString());
            return Ok(new { token,user.IsFirstConnection });
        }
        [HttpPost("set-password")]
        [RequireRole(UserRole.Student,UserRole.Admin, UserRole.Mamak, UserRole.Commander)]

        public async Task<ActionResult> SetPassword([FromBody] SetPasswordDTO setPasswordDTO)
        {

            if (HttpContext.Items[Consts.HTTP_CONTEXT_USER_ID] == null)
                return Forbid();
            string userId = HttpContext.Items[Consts.HTTP_CONTEXT_USER_ID].ToString();
            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
                return Unauthorized();

            if(user.IsFirstConnection)
                {
                user.IsFirstConnection = false;
                user.Password = setPasswordDTO.password;
                string json = $@"
                        {{
                            ""IsFirstConnection"": false,
                            ""Password"": ""{setPasswordDTO.password}""
                        }}";
                JsonElement updateElements = JsonDocument.Parse(json).RootElement;
                await _userRepo.UpdateAsync(userId, updateElements);
            }
            return Ok(new { });
        }
    }
}
