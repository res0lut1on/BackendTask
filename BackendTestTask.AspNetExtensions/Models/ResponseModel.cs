using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.AspNetExtensions.Models
{
    public class ResponseModelBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ResponseModel : ResponseModelBase
    {
        public object Result { get; set; }
    }

    public class ResponseModel<TModel> : ResponseModelBase
    {
        public TModel Result { get; set; }
    }
}
