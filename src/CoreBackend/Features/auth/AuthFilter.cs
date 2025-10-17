using CoreBackend.Features.Users;
using MongoConnection.Enums;

namespace CoreBackend.Features.auth
{
    public class AuthFilter
    {
        private Dictionary<string, UserRole> _permission;
        private static UserRole[] roles = new UserRole[] { UserRole.Admin, UserRole.Mamak, UserRole.Commander, UserRole.Student };

        public AuthFilter()
        {
            _permission = new Dictionary<string, UserRole>();
            this.InitUserPermissions();
        }
        public static string FunctionSignature(string controllerName, string functionName)
        {
            Console.WriteLine($"{controllerName}:{functionName}");
            return $"{controllerName}:{functionName}";
        }
        public bool IsAuthorized(string functionSignature, UserRole currentUserRole)
        {
            if (_permission.ContainsKey(functionSignature))
            {
                UserRole neededRole = _permission[functionSignature];
                return RoleHierachy(currentUserRole, neededRole);
            }
            return false;
        }
        public void InitUserPermissions()
        {
            _permission[FunctionSignature(nameof(UsersController),nameof(UsersController.CreateUser))] = UserRole.Student;
        }

        private bool RoleHierachy(UserRole currentRole, UserRole neededRole)
        {
            int currentIndex = Array.IndexOf(roles, currentRole);
            int neededIndex = Array.IndexOf(roles, neededRole);
            return currentIndex >= neededIndex;
        }
    }
}
