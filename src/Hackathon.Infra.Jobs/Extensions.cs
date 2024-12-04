using Hackathon.AppService.Queries.Requests.Publications;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon.Infra.Jobs
{
    public static class Extensions
    {
        public static void AddJobs(this IServiceCollection services,IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("HackathonHangFire");

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString));

            services.AddHangfireServer();
        }

        public static void ConfigureJobs(this IApplicationBuilder app, IRecurringJobManager recurringJobManager)
        {
            app.UseHangfireDashboard("/jobs");

            recurringJobManager.AddOrUpdate<IMediator>("ReadPublications_SP", x => x.Send(new ReadPublicationsQueryRequest() { Detran = SharedKernel.Enums.HackathonEnums.Detrans.SP }, default), "0 0 * * *");
        }

    }
}
