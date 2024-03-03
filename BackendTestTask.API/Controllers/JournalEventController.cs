using BackendTestTask.API.Models;
using BackendTestTask.AspNetExtensions.Models;
using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models;
using BackendTestTask.Services.Models.BaseModels;
using BackendTestTask.Services.Models.JournalEvent;
using BackendTestTask.Services.Services.Generic.Interfaces;
using BackendTestTask.Services.Services.Interfaces;
using BackendTestTask.Services.Services.SearchInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JournalEventController : ControllerBase
    {

        private readonly ILogger<JournalEventController> _logger;
        private readonly IJournalEventService _journalEventService;
        private readonly IJournalEventSearchService _searchService;
        private readonly ICustomGenericService _genericService;
        public JournalEventController(ILogger<JournalEventController> logger, IJournalEventService journalEventService, ICustomGenericService genericService, IJournalEventSearchService searchService)
        {
            _logger = logger;            
            _journalEventService = journalEventService;
            _genericService = genericService;
            _searchService = searchService;
        }

        /// <summary>
        /// Represents journal API
        /// </summary>
        /// <remarks>Provides the pagination API. Skip means the number of items should be skipped by server. Take means the maximum number items should be returned by server (DEF = 20). Filter by EventId.Contains(EventId).</remarks>
        /// <response code="200">Journal of events has been returned</response>
        /// <response code="400">Journal search string has missing/invalid values</response>
        [HttpGet("api.journal.getRange")]
        public async Task<ResponseListModelWithTotalCount<ResponseJournalEventListModel>> Get([FromQuery] RequestSearchOptions searchOptions)
        {            
            var result =
                await _genericService.SearchWithTotalCount<JournalEvent, ResponseJournalEventListModel, BaseSearchModel<string?>, string?>(
                    _searchService, searchOptions);

            return result;
        }

        /// <summary>
        /// Represents journal API
        /// </summary>
        /// <remarks>Get single journal log.</remarks>
        /// <response code="200">Journal event has been returned</response>
        /// <response code="400">Journal search string has missing/invalid values</response>
        [HttpGet("api.journal.getSingle")]
        public async Task<ResponseJournalEvent> GetSignle([FromQuery] string EventId)
        {
            var result =
                await _journalEventService.GetJournalEvent(EventId);

            return result;
        }

        /// <summary>
        /// Secure Exception
        /// </summary>
        /// <remarks>Provides the API "throw Exception logic"</remarks>       
        /// <response code="500">Throw Secure Exception Example</response>
        [HttpPost("api.journal.ThrowExampleSecureException")]
        public Task<ResponseJournalEvent> ThrowSecureException([FromQuery] string? message)
        {
            message = message is not null ? message : "Examples";
            
            var exc = new SecureException(message);
            
            _logger.LogError(exc, message);
            
            throw exc;
        }
    }
}