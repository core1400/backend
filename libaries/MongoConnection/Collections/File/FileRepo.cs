using MongoDB.Driver;

namespace MongoConnection.Collections.File
{
    public class FileRepo : Repository<File>,IFileRepo
    {
        public FileRepo(MongoContext mongoContext) : base(mongoContext,Consts.FILE_DATABASE_NAME)
        {
        }

        public async Task<File?> GetByCId(string courseId)
        {
            return await _collection.Find(file => file.Id == courseId).FirstOrDefaultAsync();
        }

        public async Task<File> GetByCNum(string courseNumber)
        {
            return await _collection.Find(file => file.CourseNumber == courseNumber ).FirstOrDefaultAsync();
        }
    }
}
