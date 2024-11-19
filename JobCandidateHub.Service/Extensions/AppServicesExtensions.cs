using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using JobCandidateHub.Service.Business;
using JobCandidateHub.Service.Repository;

namespace JobCandidateHub.Service.Extensions;
public static class AppServicesExtensions
{
    public static void AddSettings(this IServiceCollection services, ConfigurationManager configuration)
    {
        //we can add any sections...
    }
    public static void AddAppServicesDomain(this IServiceCollection services)
    {
        services.AddScoped<ICandidateBusiness, CandidateRepository>();
    }
}

