using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Models.JournalEvent
{
    public class ResponseJournalEventListModel
    {
        public int Id { get; set; }
        public string EventId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
