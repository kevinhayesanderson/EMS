using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace API.ActionFilters
{
    public class ValidateAdminRoleType : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var acceptHeaderPresent = context.HttpContext.Request.Headers.ContainsKey("RoleType");

            if (!acceptHeaderPresent)
            {
                context.Result = new BadRequestObjectResult("RoleType header is missing");
                return;
            }

            context.HttpContext.Request.Headers.TryGetValue("RoleType", out StringValues roleType);
            var roleString = roleType.ToString();
            if (roleString != "Admin")
            {
                context.Result = new UnauthorizedObjectResult($"{roleString} can not access this api endpoint!");
                return;
            }
        }
    }
}