using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
    }
}
