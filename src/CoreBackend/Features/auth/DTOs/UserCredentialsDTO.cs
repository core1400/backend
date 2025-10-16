namespace CoreBackend.Features.Auth.DTOs
{
    public class UserCredentialsDTO
    {
        public required string personalNumber { get; set; }
        public required string password { get; set; }
    }
}