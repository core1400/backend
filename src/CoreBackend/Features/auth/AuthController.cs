using CoreBackend.Features.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.Auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {

        public AuthController()
        {
            // Dependences Here
        }

        [HttpPost("sign-in")]
        public ActionResult TryAuthenticate([FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            // Code Here
        }
    }
}
