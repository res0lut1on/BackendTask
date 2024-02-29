
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;


namespace BackendTestTask.UserContext
{
    public abstract class UserContextBaseServiceFilter : IAsyncActionFilter
    {
        protected abstract IUserContext Context { get; }

        protected virtual Task SetUser(int userId, List<Claim> claims)
        {
            Context.Id = userId;

            var role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            if (role != null)
            {
                Context.Role = role.Value;
            }

            return Task.CompletedTask;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;

            if (user.Identity is { IsAuthenticated: true })
            {
                var subject = user.Identity.Name;

                if (!string.IsNullOrEmpty(subject))
                {
                    var userId = int.Parse(subject);
                    await SetUser(userId, user.Claims.ToList());
                }
            }
            else // authorization is not required for the task
            {
                await SetUser(1, new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, "TypicalNode")
                });
            }

            await next();
        }
    }

    public class UserContextServiceFilter : UserContextBaseServiceFilter
    {
        private readonly IUserContext _userContext;

        public UserContextServiceFilter(IUserContext userContext)
        {
            _userContext = userContext;
        }

        protected override IUserContext Context => _userContext;
    }

    public class UserContextServiceFilter<TUser, TUserModel> : UserContextBaseServiceFilter
        where TUser : class
        where TUserModel : class
    {
        private readonly IUserContext<TUserModel> _userContext;

        public UserContextServiceFilter(IUserContext<TUserModel> userContext)
        {
            _userContext = userContext;
        }

        protected override IUserContext Context => _userContext;

        protected override async Task SetUser(int userId, List<Claim> claims)
        {
            await base.SetUser(userId, claims);

            // example user auth, where TUser is Database Model that will be mapped to response TUserModel
            //_userContext.User = await _userService.GetAsync<TUser, TUserModel>(userId); 
        }
    }
}
