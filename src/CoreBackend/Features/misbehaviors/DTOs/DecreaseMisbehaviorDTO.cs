using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.Misbehaviors.DTOs
{
    public class DecreaseMisbehaviorDTO
    {
        public required int decreaseAmount { get; set; }
    }
}