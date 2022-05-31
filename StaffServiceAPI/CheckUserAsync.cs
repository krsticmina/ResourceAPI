using Microsoft.AspNetCore.Mvc.Filters;
using StaffServiceAPI.Controllers;
using StaffServiceBLL;
using StaffServiceCommon;
using System.Security.Claims;

namespace StaffServiceAPI
{
    public class CheckUserAsync : Attribute, IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var service = context.HttpContext.RequestServices.GetService<IEmployeeService>();

            if(service==null) throw new ArgumentNullException(nameof(service));

            var baseController = context.Controller as ApiControllerBase;

            var userId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var employee= await service.FindUserInDatabaseAsync(Convert.ToInt32(userId));

            baseController.CurrentEmployeeId = employee.Id;

            if (context.HttpContext.User.IsInRole(Position.Admin.ToString()))
            {
                baseController.IsAdmin = true;
            }

            await next();
        }
    }
}
