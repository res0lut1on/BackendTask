using BackendTestTask.Services.Services.Generic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Entities
{
    public class Node : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TreeId { get; set; }
        public Tree Tree { get; set; }
        public int? ParentNodeId { get; set; }
        public Node? ParentNode { get; set; }
        public ICollection<Node> ChildrenNodes { get; set; } = new List<Node>();
    }
}
