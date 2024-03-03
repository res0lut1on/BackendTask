using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Models.Node
{
    public class ResponseNodeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ResponseNodeModel> Children { get; set; }
    }
}
