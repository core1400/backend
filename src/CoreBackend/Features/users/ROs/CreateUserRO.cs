using MongoConnection.Collections.UserModel;

namespace CoreBackend.Features.Users.ROs
{
    public class CreateUserRO
    {
        public MongoConnection.Collections.UserModel.User user { get; set; }
    }
}