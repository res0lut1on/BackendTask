using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.UserContext
{
    public class UserContext : IUserContext // simple user model
    {
        public int? Id { get; set; }
        public string? Role { get; set; }
        public bool IsAuthenticated => Id.HasValue;
    }

    public class UserContext<TUserModel> : UserContext, IUserContext<TUserModel>
        where TUserModel : class
    {
        public TUserModel? User { get; set; }
    }
}
