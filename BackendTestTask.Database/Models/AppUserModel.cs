using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Models
{
    public class AppUserModel
    {
        public int Id { get; set; }
        public bool? PermissionForSomething { get; set; }
        public string Email { get; set; } = null!;
    }
}
