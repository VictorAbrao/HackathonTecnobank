using Hackathon.AppService.Services;
using Hackathon.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon.AppService
{
    public static class Extensions
    {
        public static void AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IPublicationsService, PublicationsService>();
        }
    }
}
