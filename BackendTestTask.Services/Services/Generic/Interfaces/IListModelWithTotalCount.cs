using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Generic.Interfaces
{
    public interface IListModelWithTotalCount<TModel>
    {
        public List<TModel> Items { get; set; }
        public int Count { get; set; }
    }
}
