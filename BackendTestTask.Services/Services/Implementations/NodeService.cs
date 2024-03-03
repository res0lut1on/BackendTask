using BackendTestTask.AspNetExtensions.Models;
using BackendTestTask.Database;
using BackendTestTask.Database.Entities;
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
    public class NodeService : INodeService
    {
        private readonly IRepository<BackendTestTaskContext> _repository;

        public NodeService(IRepository<BackendTestTaskContext> repository)
        {
            _repository = repository;
        }
        public async Task RenameNode(RequestRenameNodeModel model)
        {
            await CheckDuplicateName(model.NewNodeName);
            await IsTreeExist(model.TreeName);
            await IsNodeExist(model.NodeId);
            await IsTreeContainNode(model.NodeId, model.TreeName);

            var node = await _repository.GetAsync<Node>(n => n.Id == model.NodeId);

            node!.Name = model.NewNodeName;

            await _repository.SaveAsync();
        }
        public async Task CreateNode(RequestCreateNodeModel model)
        {
            await ValidateRequestParams(model.NodeName, model.ParentNodeId, model.TreeName);

            var parentNode = await _repository.Query<Node>()
                .Where(n => n.Id == model.ParentNodeId)
                .Include(n => n.Tree)
                .FirstOrDefaultAsync();

            var treeId = await _repository.Query<Tree>()
                .Where(tr => tr.Id == parentNode!.Tree.Id)
                .Select(tr => tr.Id)
                .FirstOrDefaultAsync();

            var newNode = new Node()
            {
                ParentNodeId = model.ParentNodeId,
                Name = model.NodeName,
                TreeId = treeId,
            };

            await _repository.AddAsync(newNode);
        }

        private async Task ValidateRequestParams(string NewNodeName, int NodeId, string TreeName)
        {
            await CheckDuplicateName(NewNodeName);
            await IsNodeExist(NodeId);
            await IsTreeExist(TreeName);
            await IsTreeContainNode(NodeId, TreeName);
        }

        private async Task IsTreeContainNode(int NodeId, string TreeName)
        {
            var isTreeContainNode = await _repository.AnyAsync<Tree>(tr => tr.Name.ToLower() == TreeName && tr.Nodes.Any(ch => ch.Id == NodeId));

            if (!isTreeContainNode)
            {
                throw new SecureException("The specified tree does not contain such a node.");
            }
        }

        private async Task IsTreeExist(string TreeName)
        {
            var checkTree = await _repository.AnyAsync<Tree>(n => n.Name.ToLower() == TreeName.ToLower());

            if (!checkTree)
            {
                throw new SecureException("The specified tree does not exist.");
            }
        }

        private async Task IsNodeExist(int ParentNodeId)
        {
            var checkParentNodeId = await _repository.AnyAsync<Node>(n => n.Id == ParentNodeId);

            if (!checkParentNodeId)
            {
                throw new SecureException("The specified node or parent node does not exist.");
            }
        }

        private async Task CheckDuplicateName(string NodeName)
        {
            var checkName = await _repository.AnyAsync<Node>(n => n.Name.ToLower() == NodeName.ToLower());

            if (checkName)
            {
                throw new SecureException("Duplicate Node Name.");
            }
        }

        public async Task RemoveNode(RequestRemoveNodeModel model)
        {
            await IsTreeExist(model.TreeName);
            await IsNodeExist(model.NodeId);

            var node = await _repository.Query<Node>()
                .Where(n => n.Id == model.NodeId)
                .Include(n => n.ChildrenNodes)
                .FirstOrDefaultAsync();

            var isItParentNode = node!.ChildrenNodes.Count != 0;

            if (isItParentNode)
            {
                throw new SecureException("This node cant be deleted. This node has child nodes");
            }

            await _repository.DeleteAsync<Node>(node.Id);
        }
    }
}
