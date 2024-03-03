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
using BackendTestTask.Database;
using BackendTestTask.Services.Services.Generic.Interfaces;

namespace BackendTestTask.Services.Services.Implementations
{
    public class SecureExceptionService : ISecureExceptionService
    {
        private readonly IRepository<BackendTestTaskContext> _repository;

        public SecureExceptionService(IRepository<BackendTestTaskContext> repository)
        {
            _repository = repository;
        }

        public async Task<ExceptionResponse> SaveLog(JournalEvent entity, Dictionary<string, string> data)
        {
            entity.EventID = GenerateEventId();
            await _repository.AddAsync(entity);

            var result = new ExceptionResponse()
            {
                Id = entity.EventID,
                Type = ExceptionTypes.Exception.Description(),
                Data = data
            };

            return result;
        }

        private static string GenerateEventId()
        {
            return new string(Guid.NewGuid().ToString().Where(char.IsDigit).ToArray());
        }

        public async Task<ExceptionResponse> SaveLog(ExceptionContext? context, Dictionary<string, string> data)
        {
            var exception = context.Exception;
            var logException =  await SetExceptionFields(context, exception);

            await _repository.AddAsync(logException);

            var result = new ExceptionResponse()
            {
                Id = logException.EventID,
                Type = exception is SecureException ? ExceptionTypes.Secure.Description() : ExceptionTypes.Exception.Description(),
                Data = data is not null ? data : new Dictionary<string, string>(){{"message", exception.Message}}
            };

            return result;
        }

        private static async Task<JournalEvent> SetExceptionFields(ExceptionContext context, Exception exception)
        {
            var exceptionLog = new JournalEvent(exception);

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

            exceptionLog.EventID = GenerateEventId();

            return exceptionLog;
        }
    }
}
