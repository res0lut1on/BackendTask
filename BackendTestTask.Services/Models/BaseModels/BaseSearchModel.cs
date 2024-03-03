using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Models.BaseModels
{
    public class BaseSearchModel<TValue> : PaginationParams
    {
        public TValue? Search { get; set; }
    }

    public class BaseSearchModel
    {
        public string Search { get; set; } = null!;
    }

}
