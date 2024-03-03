using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.Tree;
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
        Task<ResponseTreeModel> GetTreeModel(string treeName);
        Task<List<ResponseTreeModel>> GetResponseTreeModelAsync(string? treeName);
    }
}
