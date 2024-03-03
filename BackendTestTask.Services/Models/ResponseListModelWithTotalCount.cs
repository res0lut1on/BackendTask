using BackendTestTask.Services.Services.Generic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Models
{
    public class ResponseListModelWithTotalCount<TModel> : IListModelWithTotalCount<TModel> where TModel : class
    {
        public List<TModel> Items { get; set; }
        public int Count { get; set; } = 0;
    }
}
