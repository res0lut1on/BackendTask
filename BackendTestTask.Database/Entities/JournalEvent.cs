using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Entities
{
    public class JournalEvent
    {
        public int Id { get; set; }
        public string EventID { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public string QueryParameters { get; set; } = string.Empty;
        public string BodyParameters { get; set; }  = string.Empty;
        public string StackTrace { get; set; } = string.Empty;

        public JournalEvent()
        {
        }
        public JournalEvent(Exception exception)
        {
            TimeStamp = DateTime.UtcNow;
            StackTrace = exception.StackTrace ?? string.Empty;
        }
    }
}
