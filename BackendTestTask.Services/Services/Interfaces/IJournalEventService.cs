using BackendTestTask.Database.Entities;
using BackendTestTask.Services.Models.JournalEvent;
using BackendTestTask.Services.Services.Generic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Interfaces
{
    public interface IJournalEventService : IMappingsService
    {
        Task<List<ResponseJournalEventListModel>> GetListOfEvents();
        Task<ResponseJournalEvent> GetJournalEvent(string EventId);
    }
}
