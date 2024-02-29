using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTestTask.Database.Models;
using BackendTestTask.UserContext;

namespace BackendTestTask.AspNetExtensions.Filters
{
    public class UserContextFilter : IAsyncActionFilter
    {
        private readonly IUserContext<AppUserModel> _userContext;

        public UserContextFilter(IUserContext<AppUserModel> userContext)
        {
            _userContext = userContext;
        }

        // skip this step because its just example implementations of rest auth logic
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //if (_userContext.User == null)
            //{
            //    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            //}
            //else if (_userContext.User.PermissionForSomething == null)
            //{
            //    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            //}
            //else
            //{
            //    await next();
            //}
            await next();
        }
    }
}
