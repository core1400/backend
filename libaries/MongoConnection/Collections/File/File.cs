namespace MongoConnection.Collections.File
{
    public class File : BaseCollection
    {
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public byte[] Data { get; set; } = null!;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string CourseNumber { get; set; } = null!;
    }
}
