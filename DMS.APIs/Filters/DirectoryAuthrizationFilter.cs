using DMS.Core.Entities.Identity;
using DMS.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.APIs.Filters
{
    public class DirectoryAuthrizationFilter : IAuthorizationFilter
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DMSDbContext _context;

        public DirectoryAuthrizationFilter(UserManager<AppUser> userManager, DMSDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Retrieve the directory ID from route data or action parameters
            if (!context.ActionArguments.TryGetValue("directoryId", out var directoryIdObj) || directoryIdObj == null)
            {
                context.Result = new BadRequestObjectResult("Directory ID is missing.");
                return;
            }

            var directoryId = (int)directoryIdObj;

            // Get the current user
            var user = await _userManager.GetUserAsync(context.HttpContext.User);

            if (user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if the user has access to the directory
            var hasAccess = await _context.Directories
                                          .AnyAsync(d => d.Id == directoryId && d.WorkSpaceId == user.WorkSpaceID);

            if (!hasAccess)
            {
                context.Result = new ForbidResult(); // Or you can use UnauthorizedResult
                return;
            }

            // Continue executing the action
            await next();
        }

        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}

