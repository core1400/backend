using CoreBackend.Features.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection;
using MongoConnection.Collections.UserModel;
using MongoConnection.Enums;

namespace CoreBackend.Features.Auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private UserRepo _userRepo;
        public AuthController(MongoContext mongoContext)
        {
            _userRepo = new UserRepo(mongoContext);
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult> TryAuthenticate([FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            throw new NotImplementedException();
        }
    }
}
