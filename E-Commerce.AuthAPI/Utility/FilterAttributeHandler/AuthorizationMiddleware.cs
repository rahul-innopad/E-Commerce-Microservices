using E_Commerce.APIResponseLibrary.Constant.RoleManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace E_Commerce.AuthAPI.Utility.FilterAttributeHandler
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthorizationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (!IsAuthorize(context))
                {

                }
                await _next(context);
            }
            catch(Exception ex)
            {

            }
        }

        private bool IsAuthorize(HttpContext context)
        {
            return context.User.Identity.IsAuthenticated && context.User.IsInRole(MasterRoleManager.Admin);
        }
    }
}
