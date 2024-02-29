using BackendTestTask.AspNetExtensions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTestTask.Database.Models;
using BackendTestTask.Services.Services.Interfaces;

namespace BackendTestTask.AspNetExtensions.Filters
{
    public class ResponseModelFilter : IAsyncAlwaysRunResultFilter
    {
        private static readonly List<int> SuccessCodes = new() { StatusCodes.Status200OK, StatusCodes.Status201Created, StatusCodes.Status204NoContent };
        private readonly ILogger<ExceptionHandlerAttribute> _logger;
        private readonly ISecureExceptionService _secureExceptionService;
        public ResponseModelFilter(ILogger<ExceptionHandlerAttribute> logger, ISecureExceptionService secureExceptionService)
        {
            _logger = logger;
            _secureExceptionService = secureExceptionService;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var statusCode = context.HttpContext.Response.StatusCode;
            var method = context.HttpContext.Request.Method;
            var result = context.Result as ObjectResult;

            if (context.ModelState.IsValid == false)
            {
                var errors = context.ModelState.Values.SelectMany(v => v.Errors);
                
                var errorMessage = string.Join("; ", errors.Select(e => e.ErrorMessage));
                
                context.Result = new BadRequestObjectResult(new ExceptionResponse()
                {
                    Data = new Dictionary<string, string>() { { "message", "Invalid request, problem -> " + errorMessage } }
                });

                var badRequest = new SecureException("Invalid request");

                _logger.LogError(badRequest, "Request has failed.");
            }
            else if (SuccessCodes.Contains(statusCode) && result != null)
            {
                var model = result.Value;

                if ((HttpMethods.IsGet(method) || HttpMethods.IsPut(method) || HttpMethods.IsPatch(method)) && model == null)
                {
                    context.Result = new NotFoundObjectResult(new ExceptionResponse()
                    {
                        Data = new Dictionary<string, string>() {{ "message","Searching content not found." } }
                    });
                }
                else
                {
                    context.Result = new OkObjectResult(model);
                }
            }
            else if (SuccessCodes.Contains(statusCode) && result == null)
            {
                if (HttpMethods.IsDelete(method) || HttpMethods.IsPatch(method))
                {
                    context.Result = new NoContentResult();
                }
            }

            await next();
        }
    }
}
