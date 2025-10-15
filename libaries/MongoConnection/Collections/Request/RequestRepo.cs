using MongoConnection.Collections.UserModel;

namespace MongoConnection.Collections.Request
{
    public class RequestRepo : Repository<Request>, IRequestRepo
    {
        public RequestRepo(MongoContext mongoContext) : base(mongoContext)
        {
            _collection = mongoContext.GetCollection<Request>(Consts.COURSE_DATABASE_NAME);
        }
    }
}
