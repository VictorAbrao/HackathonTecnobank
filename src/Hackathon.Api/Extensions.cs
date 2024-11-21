using Hackathon.Adapter.DetranMS;
using Hackathon.Adapter.DetranSP;
using Hackathon.Infra.Adapters;
using Hackathon.Infra.ChatGPT;
using Hackathon.SharedKernel.Adapters;
using Hackathon.SharedKernel.Factories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hackathon.Api
{
    public static class Extensions
    {
        public static void AddMediatR(this IServiceCollection services, Assembly[] assemblies)
        => services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(assemblies); });

        public static void AddFactories(this IServiceCollection services)
        {
            services.AddScoped<IDetranAdapterFactory, DetranAdapterFactory>();
        }

        public static void AddAdapters(this IServiceCollection services) 
        {
            services.AddScoped<IAdapterDetran, AdapterDetranMS>();
            services.AddScoped<IAdapterDetran, AdapterDetranSP>();
            services.AddScoped<IAdapterIA, AdapterChatGPT>();
        }
    }


}
