using MongoConnection.Enums;

namespace CoreBackend.Features.Users.DTOs
{
    public class CreateUserDTO
    {
        public required string personalNum { get; set; }
        public required string Password { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public DateOnly birthDate { get; set; }
        public required string courseNum { get; set; }
        public required UserRole role { get; set; }  
    }
}