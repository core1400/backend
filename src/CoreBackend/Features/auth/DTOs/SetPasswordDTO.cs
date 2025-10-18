namespace CoreBackend.Features.auth.DTOs
{
    public class SetPasswordDTO
    {
        public string personalNumber { get; set; }
        public string newPassword { get; set; }
        public string password { get; set; }
    }
}
