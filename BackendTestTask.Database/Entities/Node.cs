using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Entities
{
    public class Node
    {
        public int Id { get; set; }
        public int NodeId { get; set; }
        public int TreeId { get; set; }
        public string NodeName { get; set; }
        public Tree Tree { get; set; }
        public int? ParentNodeId { get; set; }
        public Node ParentNode { get; set; }
        // the tree is not binary according to the task
        public ICollection<Node> ChildrenNodes { get; set; }
    }
}
