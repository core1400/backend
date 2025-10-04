namespace MongoConnection.Collections.User
{
    public interface IUser : IRepository<User>
    {
        Task<User?> GetByNameAsync(string name);
    }
}
