using MongoConnection.Collections.UserModel;

namespace CoreBackend.Features.auth.ROs
{
    public class ReturnSignIn
    {
        public User user { get; set; }  
        public string token { get; set; }
        public bool isFirstConnection { get; set; }
    }
}
