using System.Text.Json;

namespace MongoConnection.Collections.File
{
    internal interface IFileRepo: IRepository<File>
    {
        Task<File> GetByCNum(string courseNumber);
        Task<File> GetByCId(string courseId);
    }
}
