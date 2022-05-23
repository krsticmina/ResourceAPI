using Microsoft.AspNetCore.Mvc.Filters;
using StaffServiceAPI.Controllers;
using StaffServiceCommon;
using System.Security.Claims;

namespace StaffServiceAPI
{
    public class CheckUserAsync : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var employeeController = context.Controller as EmployeeController;

            var baseController = context.Controller as ApiControllerBase;

            var userId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            baseController.CurrentEmployeeId = await employeeController.FindUserInDatabaseAsync(Convert.ToInt32(userId));

            if (context.HttpContext.User.IsInRole(Position.Admin.ToString()))
            {
                baseController.IsAdmin = true;
            }

            await next();
        }
    }
}
