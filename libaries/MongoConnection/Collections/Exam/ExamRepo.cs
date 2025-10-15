using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections.Exam
{
    public class ExamRepo : Repository<Exam>, IExamRepo
    {
        public ExamRepo(MongoContext mongoContext) : base(mongoContext, Consts.EXAM_DATABASE_NAME)
        {
        }
    }
}
