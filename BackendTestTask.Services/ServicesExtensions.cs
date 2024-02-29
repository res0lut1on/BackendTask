using System.Threading.Tasks;
using BackendTestTask.Services.Services.Implementations;
using BackendTestTask.Services.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTestTask.Services
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<ISecureExceptionService, SecureExceptionService>()
                .AddScoped<INodeServices, NodeServices>();
                
            return services;
        }
    }
}