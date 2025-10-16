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
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult<List<GetUser>> GetSeveralUsers([FromQuery] UsersFilterDTO usersFilter)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpGet("{userID}")]
        public ActionResult<GetUser> GetSpecificUser(int userID)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpPatch("{userID}")]
        public ActionResult UpdateSpecificUser(int userID, [FromBody] PatchUserDTO patchUserDTO)
        {
            // Code Here
            throw new NotImplementedException();
        }

        [HttpDelete("{userID}")]
        public ActionResult RemoveSpecificUser(int userID)
        {
            // Code Here
            throw new NotImplementedException();
        }
    }
}