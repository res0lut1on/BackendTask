using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Models.Tree
{
    public class ResponseTreeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ResponseNodeModel> Children { get; set; }
    }
}
