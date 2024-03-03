using BackendTestTask.Services.Models;
using BackendTestTask.Services.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Generic.Interfaces
{
    public interface ICustomGenericService
    {
        Task<List<TModel>> Search<TEntity, TModel, TSearchModel, T>(ISearchService<TEntity, TSearchModel> searchService, TSearchModel searchOptions)
            where TEntity : class
            where TModel : class
            where TSearchModel : BaseSearchModel<T>;

        Task<ResponseListModelWithTotalCount<TModel>> SearchWithTotalCount<TEntity, TModel, TSearchModel, T>(ISearchService<TEntity, TSearchModel> searchService, TSearchModel searchOptions)
            where TEntity : class
            where TModel : class
            where TSearchModel : BaseSearchModel<T>;
    }
}
