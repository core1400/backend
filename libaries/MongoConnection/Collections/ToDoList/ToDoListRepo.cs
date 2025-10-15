using MongoConnection.Collections.UserModel;
using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections.ToDoList
{
    public class ToDoListRepo : Repository<ToDoList>, IToDoListRepo
    {
        public ToDoListRepo(MongoContext mongoContext) : base(mongoContext,Consts.TODOLIST_DATABASE_NAME)
        {
        }

        public async Task DeleteByPNumAsync(string personalNumber)
        {
            await _collection.DeleteOneAsync(toDoList => toDoList.PersonalNum == personalNumber);
        }

        public async Task<ToDoList?> GetByPNumAsync(string personalNumber)
        {
            return await _collection.Find(toDoList => toDoList.PersonalNum == personalNumber).FirstOrDefaultAsync();
        }

        public async Task UpdateByPNumAsync(string personalNumber, JsonElement updateElements)
        {
            UpdateDefinition<ToDoList> combined = GlobalTools<ToDoList>.GenericUpdate(updateElements);
            try
            {
                await base._collection.UpdateOneAsync(toDoList => toDoList.PersonalNum== personalNumber, combined);
            }
            catch (Exception e) { }
        }
    }
}
