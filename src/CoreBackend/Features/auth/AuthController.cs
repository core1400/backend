using CoreBackend.Features.auth;
using CoreBackend.Features.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection;
using MongoConnection.Collections.UserModel;

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
        public async Task<ActionResult> TryAuthenticate([FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            var user = await _userRepo.GetByPNumAsync(userCredentialsDTO.personalNumber);
            if (user == null)
                return Unauthorized();

            if (user.Password != userCredentialsDTO.password)
                return Unauthorized();

            var token = _jwtService.GenerateToken(user.Id,user.Role.ToString());
            return Ok(new { token });
        }
    }
}
