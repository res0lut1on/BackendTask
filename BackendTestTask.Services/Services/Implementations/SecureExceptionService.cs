using BackendTestTask.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTestTask.AspNetExtensions.Models;
using BackendTestTask.Common.Helpers;
using BackendTestTask.Database.Entities;
using BackendTestTask.Database.Enums;
using BackendTestTask.Database.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace BackendTestTask.Services.Services.Implementations
{
    public class SecureExceptionService : ISecureExceptionService
    {
        public async Task<ExceptionResponse> SaveLog(ExceptionContext context)
        {
            var exception = context.Exception;
            var logException =  await SetExceptionFields(context, exception);

            // get id
            var result = new ExceptionResponse()
            {
                Type = exception is SecureException ? ExceptionTypes.Secure.Description() : ExceptionTypes.Exception.Description(),
                Data = new Dictionary<string, string>(){{"message", exception.Message}}
            };

            return result;
        }

        private static async Task<ExceptionLog> SetExceptionFields(ExceptionContext context, Exception exception)
        {
            var exceptionLog = new ExceptionLog(exception);

            var queryParameters = context.HttpContext.Request.Query;

            var queryString = string.Join("&",
                queryParameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(string.Join(",", p.Value))}"));

            exceptionLog.QueryParameters = queryString;

            string requestBody;

            context.HttpContext.Request.EnableBuffering();


            // TODO:
            // need to do parse to node, tree body/query parameters modal
            using (var reader = new StreamReader(context.HttpContext.Request.Body, encoding: Encoding.UTF8,
                       detectEncodingFromByteOrderMarks: false, leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.HttpContext.Request.Body.Position = 0;
            }

            exceptionLog.BodyParameters = requestBody;

            return exceptionLog;
        }
    }
}
