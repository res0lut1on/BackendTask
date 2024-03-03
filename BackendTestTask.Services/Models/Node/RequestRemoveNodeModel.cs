using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Models.Node
{
    public class RequestRemoveNodeModel
    {
        public string TreeName { get; set; }
        public int NodeId { get; set; }
    }
}
