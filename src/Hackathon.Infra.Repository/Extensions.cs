using Hackathon.Domain.Repositories;
using Hackathon.Infra.Repository.Repositories;
using Hackathon.SharedKernel.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon.Infra.Repository
{
    public static class Extensions
    {
        public static void AddUnitOfWork(this IServiceCollection services) 
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddRepositories(this IServiceCollection services) 
        {
            services.AddScoped<IPublicationsRepository, PublicationsRepository>();
            services.AddScoped<IConciergeRepository, ConciergeRepository>();
            services.AddScoped<IKeywordsRepository, KeywordsRepository>();
        
        }
    }
}
