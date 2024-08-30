using AutoMapper;
using DMS.APIs.Dto;
using DMS.Core.Entities;
using DMS.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DMS.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryController : ControllerBase
    {
        private readonly IDirectoryService _directoryService;
        private readonly IMapper _mapper;

        public DirectoryController(IDirectoryService directoryService, IMapper mapper)
        {
            _directoryService = directoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkSpaceList()
        {
            var directories = await _directoryService.GetAllDirectories();
            if (directories == null)
            {
                return NotFound();
            }
            var directoryDtos = _mapper.Map<IEnumerable<DirectoryDto>>(directories);
            return Ok(directoryDtos);
        }

        [HttpGet("{directoryId}")]
        public async Task<IActionResult> GetProductById(int directoryId)
        {
            var directory = await _directoryService.GetDirectoryById(directoryId);
            if (directory == null)
            {
                return NotFound();
            }
            var directoryDto = _mapper.Map<DirectoryDto>(directory);
            return Ok(directoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirectory([FromBody] DirectoryDto directoryDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to entity and create directory
            var directory = _mapper.Map<Core.Entities.Directory>(directoryDto);
            var result = await _directoryService.CreateDirectory(directory);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to create directory.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDirectory([FromBody] DirectoryDto directoryDto)
        {
            if (directoryDto == null)
            {
                return BadRequest();
            }

            var directory = _mapper.Map<Core.Entities.Directory>(directoryDto);
            var isDirectoryUpdated = await _directoryService.UpdateDirectory(directory);

            if (isDirectoryUpdated)
            {
                return Ok(isDirectoryUpdated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{SoftDeletDirectoryId}")]
        public async Task<IActionResult> DeleteDirectory(int directoryId)
        {
            var isDirectoryDeleted = await _directoryService.DeleteDirectory(directoryId);

            if (isDirectoryDeleted)
            {
                return Ok(isDirectoryDeleted);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}/MakePublic")]
        public async Task<IActionResult> MakeDirectoryPublic(int id)
        {
            var result = await _directoryService.MakeDirectoryPublic(id);
            if (!result)
                return BadRequest("Directory not found or already deleted.");

            return Ok("Directory is now public.");
        }

        [HttpPut("{id}/MakePrivate")]
        public async Task<IActionResult> MakeDirectoryPrivate(int id)
        {
            var result = await _directoryService.MakeDirectoryPrivate(id);
            if (!result)
                return BadRequest("Directory not found or already deleted.");

            return Ok("Directory is now private.");
        }

        [HttpGet("GetAllNonDeleted")]
        public async Task<IActionResult> GetAllNonDeletedDirectories()
        {
            var directories = await _directoryService.GetAllNonDeletedAsync();
            var directoryDtos = _mapper.Map<IEnumerable<DirectoryDto>>(directories);
            return Ok(directoryDtos);
        }
        [HttpGet("ByUser/{userId}")]
        public async Task<IActionResult> GetDirectoriesByUserId(string userId)
        {
            var directories = await _directoryService.GetDirectoriesByUserIdAsync(userId);
            if (directories == null)
            {
                return NotFound();
            }
            return Ok(directories);
        }

    }
}
