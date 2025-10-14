using CoreBackend.Features.Users.DTOs;
using CoreBackend.Features.Users.ROs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.Users
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {

        public UsersController()
        {
            // Dependences Here
        }

        [HttpPost]
        public ActionResult<CreateUserRO> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            // Code Here
        }

        [HttpGet]
        public ActionResult<List<GetUser>> GetSeveralUsers([FromQuery] UsersFilterDTO usersFilter)
        {
            // Code Here
        }

        [HttpGet("{userID}")]
        public ActionResult<GetUser> GetSpecificUser(int userID)
        {
            // Code Here
        }

        [HttpPatch("{userID}")]
        public ActionResult UpdateSpecificUser(int userID, [FromBody] PatchUserDTO patchUserDTO)
        {
            // Code Here
        }

        [HttpDelete("{userID}")]
        public ActionResult RemoveSpecificUser(int userID)
        {
            // Code Here
        }
    }
}