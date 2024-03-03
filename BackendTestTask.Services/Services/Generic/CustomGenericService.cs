using BackendTestTask.Database;
using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models;
using BackendTestTask.Services.Models.BaseModels;
using BackendTestTask.Services.Services.Generic.Interfaces;
using BackendTestTask.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Generic
{
    public class CustomGenericService : ICustomGenericService
    {
        private readonly IRepository<BackendTestTaskContext> _repository;
        private readonly IJournalEventService _journalService;
        private readonly ITreeService _treeService;

        public CustomGenericService(IRepository<BackendTestTaskContext> repository, IJournalEventService journalService, ITreeService treeService)
        {
            _journalService = journalService;
            _repository = repository;
            _treeService = treeService;
        }

        public async Task<List<TModel>> Search<TEntity, TModel, TSearchModel, T>(
            ISearchService<TEntity, TSearchModel> searchService, TSearchModel searchOptions) where TEntity : class
            where TModel : class
            where TSearchModel : BaseSearchModel<T>
        {
            var query = _repository.Query<TEntity>();

            query = searchService.SearchLogic(query, searchOptions);

            return await GetModels<TModel, TEntity, TSearchModel>(searchOptions, query);
        }

        public async Task<ResponseListModelWithTotalCount<TModel>> SearchWithTotalCount<TEntity, TModel, TSearchModel, T>(
            ISearchService<TEntity, TSearchModel> searchService, TSearchModel searchOptions)
            where TEntity : class
            where TModel : class
            where TSearchModel : BaseSearchModel<T>
        {
            searchOptions.Take ??= 20;

            var query = _repository.Query<TEntity>();

            query = searchService.SearchLogic(query, searchOptions);

            var totalCount = await query.CountAsync();

            var models = await GetModels<TModel, TEntity, TSearchModel>(searchOptions, query);

            var result = new ResponseListModelWithTotalCount<TModel>
            {
                Items = models,
                Count = totalCount
            };

            return result;
        }

        private async Task<List<TModel>> GetModels<TModel, TEntity, TSearchModel>(TSearchModel searchOptions, IQueryable<TEntity> query)
            where TSearchModel : PaginationParams
            where TEntity : class
            where TModel : class
        {
            if (searchOptions.Skip.HasValue)
            {
                query = query.Skip(searchOptions.Skip.Value);
            }
            if (searchOptions.Take.HasValue)
            {
                query = query.Take(searchOptions.Take.Value);
            }

            List<TModel> models;

            if (typeof(TEntity) == typeof(JournalEvent))
            {
                models = await _journalService.Map<TEntity, TModel>(query).ToListAsync();
            } else
            { 
                throw new NotImplementedException();
            }

            return models;
        }
    }
}
