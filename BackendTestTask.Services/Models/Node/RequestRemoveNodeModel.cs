using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Models.Node
{
    public class RequestRemoveNodeModel
    {
        [Required]
        public string TreeName { get; set; }
        [Required]
        public int NodeId { get; set; }
    }
}
