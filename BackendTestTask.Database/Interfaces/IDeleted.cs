using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Interfaces
{
    public interface IDeleted
    {
        bool IsDeleted { get; set; }
    }
}
