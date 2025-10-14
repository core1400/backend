using CoreBackend.Features.Misbehaviors.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.Misbehaviors
{
    [ApiController]
    [Route("misbehavior")]
    public class MisbehaviorController : ControllerBase
    {

        public MisbehaviorController()
        {
            // Dependences Here
        }

        [HttpPost("~/users/{userID}/misbehavior")]
        public ActionResult IncreaseMisbehaviorForUser(int userID, IncreaseMisbehaviorDTO increaseMisbehaviorDTO)
        {
            // Code Here
        }

        [HttpDelete("~/users/{userID}/misbehavior")]
        public ActionResult DecreaseMisbehaviorForUser(int userID, [FromQuery] DecreaseMisbehaviorDTO decreaseAmount)
        {
            // Code Here
        }
    }
}
