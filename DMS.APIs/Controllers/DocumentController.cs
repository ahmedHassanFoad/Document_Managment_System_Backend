using DMS.Core.Entities;
using DMS.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DMS.APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file,  [FromForm] int directoryId)
        {
            try
            {
                // Save file to disk and get file path
                var filePath = await _documentService.SaveFileAsync(file);

                // Create and save document entity
                var document = new Document
                {
                    Name = Path.GetFileNameWithoutExtension(filePath),
                    type = Path.GetExtension(filePath),
                    Version = "1.0" ,
                    IsDeleted = false,
                    DirectoryId = directoryId,
                    path=filePath,
                    Owner ="a",
                    Date=DateTime.Now
                    
                };

                await _documentService.CreateDocument(document);

                return Ok(new { FilePath = filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDocumentsByUserId(string userId)
        {
            var documents = await _documentService.GetDocumentsByUserIdAsync(userId);
            if (documents == null || !documents.Any())
            {
                return NotFound("No documents found for the specified user.");
            }

            return Ok(documents);
        }

        [HttpGet("directory/{directoryId}")]
        public async Task<IActionResult> GetDocumentsByDirectoryId(int directoryId)
        {
            var documents = await _documentService.GetDocumentsByDirectoryIdAsync(directoryId);
            if (documents == null || !documents.Any())
            {
                return NotFound("No documents found for the specified directory.");
            }

            return Ok(documents);
        }

        [HttpGet("workspace/{workSpaceId}")]
        public async Task<IActionResult> GetDocumentsByWorkSpaceId(int workSpaceId)
        {
            var documents = await _documentService.GetDocumentsByWorkSpaceIdAsync(workSpaceId);
            if (documents == null || !documents.Any())
            {
                return NotFound("No documents found for the specified workspace.");
            }

            return Ok(documents);
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetDocumentById(int id)
        //{
        //    var document = await _documentService.GetDocumentById(id);
        //    if (document == null || document.IsDeleted)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(document);
        //}

        [HttpGet("ALLNonDeleted")]
        public async Task<IActionResult> GetAllNonDeletedDocuments()
        {
            var documents = await _documentService.GetAllNonDeletedDocumentsAsync();
            return Ok(documents);
        }

        [HttpDelete("{softDeleteById}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            await _documentService.DeleteDocument(id);
            return NoContent();
        }
        [HttpGet("count/user/{userId}")]
        public async Task<IActionResult> CountDocumentsByUser(string userId)
        {
            var count = await _documentService.CountDocumentsByUserAsync(userId);
            return Ok(count);
        }
    }
}

