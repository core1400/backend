using CoreBackend.Features.Misbehaviors.DTOs;
using CoreBackend.Features.Users.ROs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.Misbehaviors
{
    [ApiController]
    [Route("misbehavior")]
    public class MisbehaviorController : ControllerBase
    {
        private readonly IMisbehaviorService _misbehaviorService;

        public MisbehaviorController(IMisbehaviorService misbehaviorService)
        {
            _misbehaviorService = misbehaviorService;
        }

        [HttpPost("~/users/{userID}/misbehavior")]
        public async Task<ActionResult<GetUser>> IncreaseMisbehaviorForUser(string userID, IncreaseMisbehaviorDTO increaseMisbehaviorDTO)
        {
            return await _misbehaviorService.IncreaseMisbehaviorForUser(userID, increaseMisbehaviorDTO);
        }

        [HttpDelete("~/users/{userID}/misbehavior")]
        public async Task<ActionResult<GetUser>> DecreaseMisbehaviorForUser(string userID, [FromQuery] DecreaseMisbehaviorDTO decreaseAmount)
        {
            return await _misbehaviorService.DecreaseMisbehaviorForUser(userID, decreaseAmount);
        }
    }
}
