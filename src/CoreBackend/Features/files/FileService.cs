using CoreBackend.Features.files.DTOs;
using CoreBackend.Features.files.ROs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoConnection;
using MongoConnection.Collections.File;
using MongoConnection.Collections.UserModel;
using MongoDB.Driver;

namespace CoreBackend.Features.files
{
    public class FileService : IFileService
    {
        private FileRepo _fileRepo;
        private UserRepo _userRepo; 
        public FileService(MongoContext mongoContext) 
        {
            _fileRepo = new FileRepo(mongoContext);
            _userRepo = new UserRepo(mongoContext);
        }
        public async Task<ActionResult<List<MongoConnection.Collections.File.File>>> GetFile(FileFilterDTO fileFilterDTO)
        {
            FilterDefinitionBuilder<MongoConnection.Collections.File.File> builder = Builders<MongoConnection.Collections.File.File>.Filter;
            List<FilterDefinition<MongoConnection.Collections.File.File>> filters = new List<FilterDefinition<MongoConnection.Collections.File.File>>();

            if (fileFilterDTO.CourseNumber != null)
                filters.Add(builder.Eq(file => file.CourseNumber, fileFilterDTO.CourseNumber));


            var finalFilters = filters.Any() ? builder.And(filters) : builder.Empty;

            var matchingFiles = await _fileRepo.GetByFilterAsync(finalFilters);

            if (matchingFiles == null)
                return new NotFoundResult();

            return matchingFiles;
        }

        public async Task<ActionResult<GetFileRo>> GetSpecificFile(string id)
        {
            MongoConnection.Collections.File.File? file = await _fileRepo.GetByIdAsync(id);
            if (file == null)
                return new NotFoundResult();

            GetFileRo getFileRo = new GetFileRo
            {
                file = new FileContentResult(file.Data, file.ContentType)
                {
                    FileDownloadName = file.FileName
                }
            };
            return getFileRo;
        }

        public async Task<ActionResult> Upload(CreateFileDTO createFileDTO, string userId)
        {
            if (createFileDTO.file == null || createFileDTO.file.Length == 0)
                return new BadRequestResult();

            using var ms = new MemoryStream();
            await createFileDTO.file.CopyToAsync(ms);

            User? user = await _userRepo.GetByIdAsync(userId);

            var newFile = new MongoConnection.Collections.File.File
            {
                FileName = createFileDTO.file.FileName,
                ContentType = createFileDTO.file.ContentType,
                Data = ms.ToArray(),
                UploadedAt = DateTime.UtcNow,
                CourseNumber =  user==null?String.Empty: user.CourseNumber??String.Empty
            };

            await _fileRepo.CreateAsync(newFile);
            return new OkResult();
        }
    }
}
