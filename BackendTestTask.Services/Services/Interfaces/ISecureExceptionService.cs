using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTestTask.Database.Entities;
using BackendTestTask.Database.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackendTestTask.Services.Services.Interfaces
{
    public interface ISecureExceptionService
    {
        Task<ExceptionResponse> SaveLog(ExceptionContext context, Dictionary<string, string>? data = null);
        Task<ExceptionResponse> SaveLog(JournalEvent entity, Dictionary<string, string> data);

    }
}
