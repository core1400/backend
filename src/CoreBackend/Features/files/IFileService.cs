using CoreBackend.Features.files.DTOs;
using CoreBackend.Features.files.ROs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.files
{
    public interface IFileService
    {
        public Task<ActionResult> Upload(CreateFileDTO createFileDTO, string userId);
        public Task<ActionResult<List<MongoConnection.Collections.File.File>>> GetFile(FileFilterDTO fileFilterDTO);
        public Task<ActionResult<GetFileRo>> GetSpecificFile(string id);

    }
}
