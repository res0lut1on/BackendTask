using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.BaseModels;
using BackendTestTask.Services.Services.Generic.Interfaces;

namespace BackendTestTask.Services.Services.SearchInterfaces
{
    public interface IJournalEventSearchService : ISearchService<JournalEvent, BaseSearchModel<string?>> { }
    
}
