using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.Node;
using BackendTestTask.Services.Services.Generic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Interfaces
{
    public interface ITreeService
    {
        Task<ResponseNodeModel> GetTreeModel(string treeName);
        Task<List<ResponseNodeModel>> GetResponseTreeModelAsync(string? treeName);
    }
}
