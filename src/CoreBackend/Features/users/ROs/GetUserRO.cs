using MongoConnection.Collections.UserModel;

namespace CoreBackend.Features.Users.ROs
{
    public class GetUser
    {
        public MongoConnection.Collections.UserModel.User? User { get; set; }
    }
}