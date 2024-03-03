using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.BaseModels;
using BackendTestTask.Services.Services.SearchInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.SearchImplementations
{
    public class TreeSearchService : ITreeSearchService
    {
        public IQueryable<Tree> SearchLogic(IQueryable<Tree> query, BaseSearchModel<string?> search)
        {
            if (string.IsNullOrEmpty(search.Search) == false)
            {
                search.Search = search.Search.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(search.Search));
            }

            query = query.OrderBy(x => x.Name);

            return query;
        }
    }
}
