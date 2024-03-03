using BackendTestTask.API.Models;
using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.BaseModels;
using BackendTestTask.Services.Models.JournalEvent;
using BackendTestTask.Services.Models;
using BackendTestTask.Services.Services.Generic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendTestTask.Controllers;
using BackendTestTask.Services.Services.Interfaces;
using BackendTestTask.Services.Services.SearchInterfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using BackendTestTask.Services.Models.Node;

namespace BackendTestTask.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly ILogger<JournalEventController> _logger;
        private readonly ITreeSearchService _searchService;
        private readonly ITreeService _treeService;
        private readonly ICustomGenericService _genericService;

        public TreeController(ICustomGenericService genericService, ITreeSearchService searchService, ITreeService treeService)
        {
            _genericService = genericService;
            _searchService = searchService;
            _treeService = treeService;
        }

        /// <summary>
        /// Represents entire tree API
        /// </summary>
        /// <remarks>Returns your entire tree. If your tree doesn't exist it will be created automatically.</remarks>
        /// <response code="200">Tree has been created/returned</response>
        /// <response code="400">Tree has missing/invalid values</response>
        /// <response code="500">Oops! Can't create your tree right now</response>
        [HttpPost("api.tree.getSingle")]
        public async Task<ResponseNodeModel> GetOrCreateTreeModel([FromQuery] string treeName)
        {
            var result = await _treeService.GetTreeModel(treeName);

            return result;
        }

        /// <summary>
        /// List of trees
        /// </summary>
        /// <remarks>Returns your list of trees.</remarks>
        /// <response code="200">List of trees has been returned</response>
        /// <response code="400">Tree search string has missing/invalid values</response>
        [HttpGet("api.tree.getRange")]
        public async Task<List<ResponseNodeModel>> Get([FromQuery] string? treeName)
        {
            var result = await _treeService.GetResponseTreeModelAsync(treeName);

            return result;
        }
    }
}
