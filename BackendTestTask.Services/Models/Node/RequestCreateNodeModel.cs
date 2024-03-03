using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Models.Node
{
    public class RequestCreateNodeModel
    {
        [Required]
        public string TreeName { get; set; } = null!;
        [Required]
        public int ParentNodeId { get; set; }
        [Required]
        public string NodeName { get; set; } = null!;
    }
}
