using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Models
{
    public class ExceptionResponse
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }
}
