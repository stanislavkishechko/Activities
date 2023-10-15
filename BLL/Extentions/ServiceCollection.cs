using BLL.Activities.Commands.CreateActivity;
using BLL.Activities.Queries.GetActivitiesList;
using BLL.Interfaces;
using BLL.Mapping;
using DAL.Security;
using FluentValidation;
using FluentValidation.AspNetCore;
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
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateActivityCommand>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();

            return services;
        }
    }
}
