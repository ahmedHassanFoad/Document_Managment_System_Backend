using AutoMapper;
using DMS.APIs.Dto;
using DMS.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.APIs.Controllers
{
  //  [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]


    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AdminController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("ListUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> ListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users); // Correctly map the list of users
            return Ok(userDtos);
        }

        [HttpPost("LockUser/{userId}")]
        public async Task<IActionResult> LockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
            await _userManager.UpdateAsync(user);

            return Ok("User locked");
        }

        [HttpPost("UnlockUser/{userId}")]
        public async Task<IActionResult> UnlockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);

            return Ok("User unlocked");
        }

        [HttpGet("UserDetails/{userId}")]
        public async Task<ActionResult<UserDetailsDto>> UserDetails(string userId)
        {
            var user = await _userManager.Users
                .Include(u => u.WorkSpace) // Make sure to include the WorkSpace
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return NotFound("User not found");

            var userDetailsDto = _mapper.Map<UserDetailsDto>(user);
            return Ok(userDetailsDto);
        }




    }
}

