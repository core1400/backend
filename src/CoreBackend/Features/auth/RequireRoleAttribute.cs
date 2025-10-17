using CoreBackend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoConnection.Enums;
using System.Security.Claims;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class RequireRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly UserRole[] _roles;

    public RequireRoleAttribute(params UserRole[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var roleClaim = context.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (roleClaim == null || !Enum.TryParse<UserRole>(roleClaim, out var userRole))
        {
            context.Result = new JsonResult(new { message = "Missing or invalid token" })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return;
        }

        if (!_roles.Contains(userRole))
        {
            context.Result = new JsonResult(new { message = "Invalid role" })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            }; return;
        }
        context.HttpContext.Items[Consts.HTTP_CONTEXT_USER_ROLE] = userRole;
    }
}