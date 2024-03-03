using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Models.Node
{
    public class RequestRenameNodeModel
    {
        [Required]
        public string TreeName { get; set; } = null!;
        [Required]
        public int NodeId { get; set; }
        [Required]
        public string NewNodeName { get; set; } = null!;
    }
}
