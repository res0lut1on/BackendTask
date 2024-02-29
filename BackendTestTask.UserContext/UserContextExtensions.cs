using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTestTask.UserContext
{
    public static class UserContextExtensions
    {
        public static IServiceCollection AddUserContext(this IServiceCollection services) // simple implementation of user context
        {
            return services
                .AddScoped<IUserContext, UserContext>()
                .AddScoped<UserContextServiceFilter>();
        }

        public static IServiceCollection AddUserContext<TUser, TUserModel>(this IServiceCollection services) // generic implementation of user context
            where TUser : class
            where TUserModel : class
        {
            return services
                .AddUserContext()
                .AddScoped<IUserContext<TUserModel>, UserContext<TUserModel>>()
                .AddScoped<UserContextServiceFilter<TUser, TUserModel>>(); 
        }

        public static void AddUserContext(this MvcOptions options)
        {
            options.Filters.Add<UserContextServiceFilter>();  // user filter
        }

        public static void AddUserContext<TUser, TUserModel>(this MvcOptions options)
            where TUser : class
            where TUserModel : class
        {
            options.Filters.Add<UserContextServiceFilter<TUser, TUserModel>>(); // user generic filter
        }
    }
}
