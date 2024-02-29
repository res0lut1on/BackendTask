using BackendTestTask.AspNetExtensions.Models;
using BackendTestTask.Common.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BackendTestTask.AspNetExtensions.Filters
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionHandlerAttribute> _logger;

        public ExceptionHandlerAttribute(ILogger<ExceptionHandlerAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Request has failed.");

            var problem = new ExceptionResponse()
            {
                Message = context.Exception.Message
            };

            var response = context.HttpContext.Response;

            if (!response.HasStarted)
            {
                if (context.Exception.GetType() == typeof(NotFoundException))
                {
                    response.StatusCode = StatusCodes.Status404NotFound;
                }
                else if (context.Exception.GetType() == typeof(ManualException))
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else
                {
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                context.Result = new JsonResult(problem);
            }

            base.OnException(context);
        }
    }
}
