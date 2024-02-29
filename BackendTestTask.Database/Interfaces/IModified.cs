using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Interfaces
{
    public interface IModified<TYpe>
    {
        TYpe ModifiedBy { get; set; }
        DateTime ModifiedOn { get; set; }
    }

    public interface IModified : IModified<int>
    {

    }
}
