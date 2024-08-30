using DMS.Core.Entities;
using DMS.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DMS.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSpaceCotroller : ControllerBase
    {
        public readonly IWorkSpaceService _workSpaceService;

        public WorkSpaceCotroller(IWorkSpaceService workSpaceService)
        {
            _workSpaceService = workSpaceService;

        }
        [HttpGet]
        public async Task<IActionResult> GetWorkSpaceList()
        {
            var workSpacessList = await _workSpaceService.GetAllWorkSpaces();
            if (workSpacessList == null)
            {
                return NotFound();
            }
            return Ok(workSpacessList);
        }
        [HttpGet("{workSpaceId}")]
        public async Task<IActionResult> GetWorkSpaceById(int WoekSpaceId)
        {
            var workSpasesdet = await _workSpaceService.GetWorkSpaceById(WoekSpaceId);

            if (workSpasesdet != null)
            {
                return Ok(workSpasesdet);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkSpace(WorkSpace WorkSpaceDetails)
        {
            var isWorkSpaceCreated = await _workSpaceService.CreateworkSpace(WorkSpaceDetails);

            if (isWorkSpaceCreated)
            {
                return Ok(isWorkSpaceCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWorkSpace(WorkSpace WorkSpaceDetails)
        {
            if (WorkSpaceDetails != null)
            {
                var isWorkSpaceCreated = await _workSpaceService.UpdateworkSpace(WorkSpaceDetails);
                if (isWorkSpaceCreated)
                {
                    return Ok(isWorkSpaceCreated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{WorkSpaceId}")]
        public async Task<IActionResult> DeleteWorkSpace(int WorkSpaceId)
        {
            var isworkSpaceDeleted = await _workSpaceService.DeleteworkSpace(WorkSpaceId);

            if (isworkSpaceDeleted)
            {
                return Ok(isworkSpaceDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
