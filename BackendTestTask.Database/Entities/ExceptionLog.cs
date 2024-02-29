using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Entities
{
    public class ExceptionLog
    {
        public string EventID { get; set; }
        public DateTime Timestamp { get; set; } 
        public string QueryParameters { get; set; } 
        public string BodyParameters { get; set; } 
        public string StackTrace { get; set; }

        public ExceptionLog(Exception exception)
        {
            Timestamp = DateTime.UtcNow;
            StackTrace = exception.StackTrace ?? string.Empty;
        }
    }
}
