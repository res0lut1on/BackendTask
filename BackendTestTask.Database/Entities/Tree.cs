using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BackendTestTask.Database.Entities
{
    public class Tree
    {
        public int Id { get; set; }
        public int TreeId { get; set; }
        public string TreeName { get; set; }
        public int? ParentNodeID { get; set; } 
        public Node ParentNode { get; set; }
        public ICollection<Node> Nodes { get; set; }
    }
}
