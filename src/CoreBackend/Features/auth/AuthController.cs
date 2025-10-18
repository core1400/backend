using CoreBackend.Features.auth;
using CoreBackend.Features.auth.DTOs;
using CoreBackend.Features.auth.ROs;
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
        public async Task<ActionResult<ReturnSignIn>> TryAuthenticate([FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            var user = await _userRepo.GetByPNumAsync(userCredentialsDTO.personalNumber);
            if (user == null)
                return Unauthorized();
            
            if (user.Password != userCredentialsDTO.password)
                return Unauthorized();
            ReturnSignIn returnSignIn = new ReturnSignIn();
            returnSignIn.user = user;

            if (user.IsFirstConnection)
            {
                returnSignIn.token = "";
                returnSignIn.isFirstConnection = user.IsFirstConnection;
                return returnSignIn;
            }
            returnSignIn.isFirstConnection = user.IsFirstConnection;
            var token = _jwtService.GenerateToken(user.Id,user.Role.ToString());
            returnSignIn.token = token;
            return returnSignIn;
        }
        [HttpPost("set-password")]

        public async Task<ActionResult> SetPassword([FromBody] SetPasswordDTO setPasswordDTO)
        {
            var user = await _userRepo.GetByPNumAsync(setPasswordDTO.personalNumber);

            if(setPasswordDTO.password != user?.Password)
                return Unauthorized();

            if (user == null)
                return Unauthorized();

            Console.WriteLine( "going to update ");
            if(user.IsFirstConnection)
                {
                    
                string json = $@"
                        {{
                            ""IsFirstConnection"": false,
                            ""Password"": ""{setPasswordDTO.newPassword}""
                        }}";
                JsonElement updateElements = JsonDocument.Parse(json).RootElement;
                await _userRepo.UpdateByPNumAsync(setPasswordDTO.personalNumber, updateElements);
            }
            return Ok(new { });
        }
    }
}
