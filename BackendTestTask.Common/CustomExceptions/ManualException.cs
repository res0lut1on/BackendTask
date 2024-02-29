using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Common.CustomExceptions
{
    public class ManualException : Exception
    {
        private const string BaseExceptionMessage = "Invalid request";
        public ManualException() : base(BaseExceptionMessage) { }
        public ManualException(string message) : base(message) { }
    }
}
