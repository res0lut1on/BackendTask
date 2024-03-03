using BackendTestTask.Common.CustomExceptions;
using BackendTestTask.Database;
using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.JournalEvent;
using BackendTestTask.Services.Models.Node;
using BackendTestTask.Services.Models.Tree;
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

        public async Task<ResponseTreeModel> GetTreeModel(string treeName)
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

                return new ResponseTreeModel
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

            var treeModel = new ResponseTreeModel
            {
                Id = tree.Id,
                Name = tree.Name,
                Children = BuildNodeModels(tree.Nodes).Where(n => n.Id == tree.Id).ToList()
            };

            return treeModel;
        }

        public async Task<List<ResponseTreeModel>> GetResponseTreeModelAsync(string? treeName)
        {
            
            var query = _repository.Query<Tree>();
            
            var trees = await query.Where(tr => treeName == null || tr.Name.ToLower().Contains(treeName.ToLower()))
                .Include(t => t.Nodes)
                .ThenInclude(n => n.ChildrenNodes)
                .ToListAsync();
                
            List<ResponseTreeModel> result = trees.Select(tree => new ResponseTreeModel
            {
                Id = tree.Id,
                Name = tree.Name,
                Children = BuildNodeModels(tree.Nodes.Where(n => n.ParentNode != null).ToList())//.Where(n => n.Id == tree.Id).ToList()
            }).ToList();

            return result;
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
