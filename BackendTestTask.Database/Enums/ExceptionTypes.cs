using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Enums
{
    public enum ExceptionTypes
    {
        [Description("Exception")]
        Exception = 0,
        [Description("Secure")]
        Secure = 1,
    }
}
