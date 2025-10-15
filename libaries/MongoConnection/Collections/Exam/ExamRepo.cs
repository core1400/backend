namespace MongoConnection.Collections.Exam
{
    public class ExamRepo : Repository<Exam>, IExamRepo
    {
        public ExamRepo(MongoContext mongoContext) : base(mongoContext)
        {
            _collection = mongoContext.GetCollection<Exam>(Consts.COURSE_DATABASE_NAME);
        }
    }
}
