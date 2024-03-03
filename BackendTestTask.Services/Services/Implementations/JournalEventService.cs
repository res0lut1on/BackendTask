using BackendTestTask.Common.CustomExceptions;
using BackendTestTask.Database;
using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.JournalEvent;
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
    public class JournalEventService : IJournalEventService, IMappingsService
    {
        private readonly IRepository<BackendTestTaskContext> _repository;

        public JournalEventService(IRepository<BackendTestTaskContext> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseJournalEvent> GetJournalEvent(string EventId)
        {
            var result = await _repository.Query<JournalEvent>()
                .Where(x => x.EventID == EventId)
                .Select(e => new ResponseJournalEvent()
            {
                EventId = e.EventID,
                CreatedAt = e.TimeStamp,
                Text = e.StackTrace,
                Id = e.Id
            }).FirstOrDefaultAsync();

            if (result is null)
            {   
                throw new NotFoundException();
            }

            return result;
        }

        public Task<List<ResponseJournalEventListModel>> GetListOfEvents()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TModel> Map<TEntity, TModel>(IQueryable<TEntity> query) where TModel : class 
        {
            if (typeof(TModel) == typeof(ResponseJournalEventListModel))
            {
                var result =  MapToResponseSingleModel((IQueryable<JournalEvent>)query);
                return result.Cast<TModel>();
            }
            else
            {
                throw new NotImplementedException($"Mapping for {typeof(TModel).Name} is not implemented.");
            }
        }

        private IQueryable<ResponseJournalEventListModel> MapToResponseSingleModel(IQueryable<JournalEvent> query)
        {
            var result = query.Select(q => new ResponseJournalEventListModel()
            {
                Id = q.Id, 
                EventId = q.EventID.ToString(),
                CreatedAt = q.TimeStamp
            });

            return result;
        }
    }
}
