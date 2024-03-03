using System.Threading.Tasks;
using BackendTestTask.Services.Services.Generic;
using BackendTestTask.Services.Services.Generic.Implementations;
using BackendTestTask.Services.Services.Generic.Interfaces;
using BackendTestTask.Services.Services.Implementations;
using BackendTestTask.Services.Services.Interfaces;
using BackendTestTask.Services.Services.SearchImplementations;
using BackendTestTask.Services.Services.SearchInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTestTask.Services
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<ISecureExceptionService, SecureExceptionService>()
                .AddScoped<IJournalEventService, JournalEventService>()
                .AddScoped<ITreeService, TreeService>()
                .AddScoped<ICustomGenericService, CustomGenericService>()
                .AddScoped< IJournalEventSearchService, JournalEventSearchService>()
                .AddScoped<ITreeSearchService, TreeSearchService>()
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<INodeService, NodeService>();
                
            return services;
        }
    }
}