using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.BaseModels;
using BackendTestTask.Services.Models.JournalEvent;
using BackendTestTask.Services.Models;
using BackendTestTask.Services.Services.Generic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTestTask.Services.Models.Node;

namespace BackendTestTask.Services.Services.Interfaces
{
    public interface INodeService
    {
        Task CreateNode(RequestCreateNodeModel model);
        Task RenameNode(RequestRenameNodeModel model);
        Task RemoveNode(RequestRemoveNodeModel model);

    }
}
