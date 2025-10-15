using MongoConnection.Collections.UserModel;

namespace MongoConnection.Collections.ResponseGrade
{
    public class ResponseGradeRepo : Repository<ResponseGrade>, IResponseGradeRepo
    {
        public ResponseGradeRepo(MongoContext mongoContext) : base(mongoContext)
        {
            _collection = mongoContext.GetCollection<ResponseGrade>(Consts.COURSE_DATABASE_NAME);
        }
    }
}
