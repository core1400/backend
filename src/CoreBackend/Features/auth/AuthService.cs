using MongoConnection.Enums;

namespace CoreBackend.Features.auth
{
    public class AuthService
    {
        public AuthService() { }
        public static bool DenyUserAccess(UserRole userRole)
        {
            if(userRole == UserRole.Student)
                return false;
            return true;
        }
    }
}