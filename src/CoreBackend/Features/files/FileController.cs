using CoreBackend.Features.files.DTOs;
using CoreBackend.Features.files.ROs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection.Enums;
using ZstdSharp.Unsafe;

namespace CoreBackend.Features.files
{
        [ApiController]
        [Route("files")]
    public class FileController : Controller
    {
        private readonly IFileService _fileService; 
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost()]
        [RequireRole(UserRole.Admin, UserRole.Commander, UserRole.Mamak,UserRole.Student)]
        public async Task<ActionResult> Upload(CreateFileDTO createFileDTO)
        {
            string? userId = HttpContext.Items[Consts.HTTP_CONTEXT_USER_ID] as string;

            return await _fileService.Upload(createFileDTO,userId);
        }

        [HttpGet()]
        [RequireRole(UserRole.Admin, UserRole.Commander, UserRole.Mamak, UserRole.Student)]

        public async Task<ActionResult<List<MongoConnection.Collections.File.File>>> GetFile([FromQuery]FileFilterDTO fileFilterDTO)
        {
            return await _fileService.GetFile(fileFilterDTO);
        }
        [HttpGet("~/getFile/{id}")]
        [RequireRole(UserRole.Admin, UserRole.Commander, UserRole.Mamak, UserRole.Student)]

        public async Task<ActionResult<GetFileRo>> GetSpecificFile(string id)
        {
            return await _fileService.GetSpecificFile(id);
        }
    }
}
