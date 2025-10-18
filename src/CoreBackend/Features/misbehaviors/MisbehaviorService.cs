using CoreBackend.Features.Misbehaviors.DTOs;
using CoreBackend.Features.Users.ROs;
using MongoConnection.Collections.UserModel;
using Microsoft.AspNetCore.Mvc;
using MongoConnection;
using System.Text.Json;

namespace CoreBackend.Features.Misbehaviors
{
    public class MisbehaviorService : IMisbehaviorService
    {
        private readonly UserRepo _userRepo;

        public MisbehaviorService(MongoContext mongoContext)
        {
            _userRepo = new UserRepo(mongoContext);
        }

        public async Task<ActionResult<GetUser>> IncreaseMisbehaviorForUser(string userID, IncreaseMisbehaviorDTO increaseMisbehaviorDTO)
        {
            User? user = await _userRepo.GetByIdAsync(userID);
            if (user == null)
                return new NotFoundResult();

            int currentCount = user.MisbehaviorCount ?? 0;
            int newCount = currentCount + increaseMisbehaviorDTO.increaseAmount;

            JsonElement json = JsonSerializer.SerializeToElement(new { MisbehaviorCount = newCount });
            await _userRepo.UpdateByPNumAsync(user.PersonalNumber, json);

            return new GetUser { User = user };
        }

        public async Task<ActionResult<GetUser>> DecreaseMisbehaviorForUser(string userID, DecreaseMisbehaviorDTO decreaseAmount)
        {
            User? user = await _userRepo.GetByIdAsync(userID);
            if (user == null)
                return new NotFoundResult();

            int currentCount = user.MisbehaviorCount ?? 0;
            int newCount = Math.Max(0, currentCount - decreaseAmount.decreaseAmount);

            JsonElement json = JsonSerializer.SerializeToElement(new { MisbehaviorCount = newCount });
            await _userRepo.UpdateByPNumAsync(user.PersonalNumber, json);

            user.MisbehaviorCount = newCount;

            return new GetUser { User = user };
        }
    }
}
