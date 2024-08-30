using DMS.APIs.Dto;
using DMS.Core.Entities.Identity;
using DMS.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using DMS.Repository.Data;

namespace DMS.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly DMSDbContext _context;

        public AccountsController(UserManager<AppUser> userManager,
         SignInManager<AppUser> signInManager, ITokenService tokenService,
         IMapper mapper, DMSDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            // Check if the email already exists
            var emailExists = await CheckEmailExists(model.Email);
            if (emailExists.Value)
            {
                return BadRequest("Email already exists.");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Check if the workspace exists; if not, create it
                    var workSpace = await _context.WorkSpaces
                        .FirstOrDefaultAsync(ws => ws.Name == model.WorkSpaceName);

                    if (workSpace == null)
                    {
                        workSpace = new WorkSpace { Name = model.WorkSpaceName };
                        _context.WorkSpaces.Add(workSpace);
                        await _context.SaveChangesAsync(); // Save to get the WorkSpaceId
                    }

                    // Create the user
                    var user = new AppUser
                    {
                        Name = model.Name,
                        Email = model.Email,
                        UserName = model.Email.Split('@')[0],
                        PhoneNumber = model.PhoneNumber,
                        NID = model.NID,
                        WorkSpaceID = workSpace.Id // Assign the WorkSpaceId to the user
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }

                    // Generate and return the token
                    var returnedUser = new UserDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        NID = user.NID,
                        workSpaceID = user.WorkSpaceID,
                        Token = await _tokenService.CreateTokenAsync(user, _userManager)
                    };

                    await transaction.CommitAsync();
                    return Ok(returnedUser);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Internal server error");
                }
            }
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)

        {

            var user = await _userManager.Users
                                 .Include(u => u.WorkSpace) // Ensure WorkSpace is loaded
                                 .FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null) return Unauthorized("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid email or password.");

            var returnedUser = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                NID = user.NID,
                workSpaceID = user.WorkSpaceID,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

            return Ok(returnedUser);
        }
    }
}
