using BackendTestTask.AspNetExtensions.Models;
using BackendTestTask.Common.CustomExceptions;
using BackendTestTask.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using BackendTestTask.Database.Models;
using BackendTestTask.Services.Services.Interfaces;

namespace BackendTestTask.AspNetExtensions.Filters
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionHandlerAttribute> _logger;
        private readonly ISecureExceptionService _secureExceptionService;
        public ExceptionHandlerAttribute(ILogger<ExceptionHandlerAttribute> logger, ISecureExceptionService secureExceptionService)
        {
            _logger = logger;
            _secureExceptionService = secureExceptionService;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Request has failed.");

            var response = context.HttpContext.Response;

            if (!response.HasStarted)
            {
                var result = await _secureExceptionService.SaveLog(context);

                response.StatusCode = StatusCodes.Status500InternalServerError;
                
                context.Result = new JsonResult(result);
            }

            await base.OnExceptionAsync(context);
        }
    }
}
