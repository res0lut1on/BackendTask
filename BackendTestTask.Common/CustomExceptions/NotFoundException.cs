using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Common.CustomExceptions
{
    public class NotFoundException : Exception
    {
        private const string BaseExceptionMessage = "Сontent not found";
        public NotFoundException() : base(BaseExceptionMessage) { }
        public NotFoundException(string message) : base(message) { }
    }
}
