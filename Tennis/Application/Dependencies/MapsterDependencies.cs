using Mapster;
using MapsterMapper;
using System.Reflection;

namespace Tennis.Application.Dependencies
{
    public static class MapsterDependencies
    {
        public static IServiceCollection AddMappingProfile(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
