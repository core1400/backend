using MongoConnection.Collections.UserModel;

namespace MongoConnection.Collections.ToDoList
{
    public class ToDoListRepo : Repository<ToDoList>, IToDoListRepo
    {
        public ToDoListRepo(MongoContext mongoContext) : base(mongoContext)
        {
            _collection = mongoContext.GetCollection<ToDoList>(Consts.COURSE_DATABASE_NAME);
        }
    }
}
