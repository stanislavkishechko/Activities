using BLL.Activities.Queries.GetActivitiesList;
using BLL.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extentions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetActivityListQueryHandler).Assembly));
            services.AddAutoMapper(typeof(AssemblyMappingProfile).Assembly);

            return services;
        }
    }
}
