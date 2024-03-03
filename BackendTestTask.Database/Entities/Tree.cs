using BackendTestTask.Services.Services.Generic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BackendTestTask.Database.Entities
{
    public class Tree : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Node> Nodes { get; set; } = new List<Node>();
    }
}
