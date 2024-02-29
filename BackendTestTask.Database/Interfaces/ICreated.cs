using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Interfaces
{
    public interface ICreated<TYpe>
    {
        TYpe CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
    }
    public interface ICreated : ICreated<int> 
    {
    }
}
