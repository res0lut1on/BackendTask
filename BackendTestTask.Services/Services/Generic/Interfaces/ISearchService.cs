using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Generic.Interfaces
{
    public interface ISearchService<TEntity, TSearchModel>
        where TEntity : class
        where TSearchModel : class
    {
        IQueryable<TEntity> SearchLogic(IQueryable<TEntity> query, TSearchModel search);
    }
}
