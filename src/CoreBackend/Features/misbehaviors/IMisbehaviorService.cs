using CoreBackend.Features.Misbehaviors.DTOs;
using CoreBackend.Features.Users.ROs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.Misbehaviors
{
    public interface IMisbehaviorService
    {
        Task<ActionResult<GetUser>> IncreaseMisbehaviorForUser(string userID, IncreaseMisbehaviorDTO dto);
        Task<ActionResult<GetUser>> DecreaseMisbehaviorForUser(string userID, DecreaseMisbehaviorDTO dto);
    }
}
