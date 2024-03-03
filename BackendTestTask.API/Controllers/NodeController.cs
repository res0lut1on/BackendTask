using BackendTestTask.Controllers;
using BackendTestTask.Services.Models.Node;
using BackendTestTask.Services.Services.Generic.Interfaces;
using BackendTestTask.Services.Services.Implementations;
using BackendTestTask.Services.Services.Interfaces;
using BackendTestTask.Services.Services.SearchInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.API.Controllers
{
    public class NodeController
    {
        private readonly INodeService _nodeService;

        public NodeController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        /// <summary>
        /// Represents tree node API
        /// </summary>
        /// <remarks>Create a new node in your tree. You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.</remarks>
        /// <response code="200">Node has been created</response>
        /// <response code="500">Node cant be created</response>
        [HttpPost("api.tree.node.create")]
        public async Task Get([FromQuery] RequestCreateNodeModel req)
        {
            await _nodeService.CreateNode(req);
        }

        /// <summary>
        /// Represents tree node API
        /// </summary>
        /// <remarks>Rename an existing node in your tree. You must specify a node ID that belongs your tree. A new name of the node must be unique across all siblings.</remarks>
        /// <response code="200">Node has been renamed</response>
        /// <response code="500">Node cant be renamed</response>
        [HttpPost("api.tree.node.rename")]
        public async Task Rename([FromQuery] RequestRenameNodeModel req)
        {
            await _nodeService.RenameNode(req);
        }

        /// <summary>
        /// Represents tree node API
        /// </summary>
        /// <remarks>Delete an existing node in your tree. You must specify a node ID that belongs your tree.</remarks>
        /// <response code="200">Node has been removed</response>
        /// <response code="500">Node cant be removed</response>
        [HttpDelete("api.tree.node.delete")]
        public async Task Remove([FromQuery] RequestRemoveNodeModel req)
        {
            await _nodeService.RemoveNode(req);
        }
    }
}
