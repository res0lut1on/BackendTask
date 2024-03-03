using BackendTestTask.Common.CustomExceptions;
using BackendTestTask.Database;
using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.JournalEvent;
using BackendTestTask.Services.Models.Node;
using BackendTestTask.Services.Services.Generic.Interfaces;
using BackendTestTask.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Implementations
{
    public class TreeService : ITreeService
    {
        private readonly IRepository<BackendTestTaskContext> _repository;

        public TreeService(IRepository<BackendTestTaskContext> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseNodeModel> GetTreeModel(string treeName)
        {
            var check = await _repository.AnyAsync<Tree>(x => x.Name.ToLower() == treeName.ToLower());

            if (!check)
            {
                var newTree = new Tree()
                {
                    Name = treeName,
                    Nodes = new List<Node>()
                    {
                        new Node()
                        {
                            Name = treeName,
                            ParentNodeId = null
                        }
                    }
                };

                await _repository.AddAsync(newTree);

                return new ResponseNodeModel
                {
                    Id = newTree.Id,
                    Name = newTree.Name
                };
            }

            var tree = await _repository.Query<Tree>()
                .Where(t => t.Name.ToLower() == treeName.ToLower())
                .Include(t => t.Nodes)
                .FirstOrDefaultAsync();
           
            if (tree == null)
            {
                throw new NotFoundException(treeName);
            }

            var treeModel = new ResponseNodeModel
            {
                Id = tree.Id,
                Name = tree.Name,
                Children = BuildNodeModels(tree.Nodes).Where(n => n.Id == tree.Id).ToList()
            };

            return treeModel;
        }

        public async Task<List<ResponseNodeModel>> GetResponseTreeModelAsync(string? treeName)
        {
            
            var query = _repository.Query<Tree>();
            
            var trees = await query.Where(tr => treeName == null || tr.Name.ToLower().Contains(treeName.ToLower()))
                .Include(t => t.Nodes)
                .ThenInclude(n => n.ChildrenNodes)
                .ToListAsync();

            List<ResponseNodeModel> responseTrees = new List<ResponseNodeModel>();

            foreach (var tree in trees)
            {
                foreach (var node in tree.Nodes)
                {
                    if (node.ParentNodeId == null) 
                    {
                        ResponseNodeModel rootNode = ConvertToResponseNodeModel(node);
                        responseTrees.Add(rootNode);
                    }
                }
            }

            return responseTrees;
        }
        private ResponseNodeModel ConvertToResponseNodeModel(Node node)
        {
            var responseNode = new ResponseNodeModel
            {
                Id = node.Id,
                Name = node.Name,
                Children = new List<ResponseNodeModel>()
            };

            foreach (var childNode in node.ChildrenNodes)
            {
                ResponseNodeModel child = ConvertToResponseNodeModel(childNode);
                responseNode.Children.Add(child);
            }

            return responseNode;
        }
        private List<ResponseNodeModel> BuildNodeModels(ICollection<Node> nodes)
        {
            return nodes.Select(node => new ResponseNodeModel
            {
                Id = node.Id,
                Name = node.Name,
                Children = BuildNodeModels(node.ChildrenNodes)
            }).ToList();
        }
    }
}
