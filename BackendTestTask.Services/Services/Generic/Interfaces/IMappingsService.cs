using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Generic.Interfaces
{
    public interface IMappingsService
    {
        IQueryable<TModel> Map<TEntity, TModel>(IQueryable<TEntity> query) where TModel : class;
    }
}
