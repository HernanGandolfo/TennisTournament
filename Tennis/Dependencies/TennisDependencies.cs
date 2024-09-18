using Mapster;
using Microsoft.OpenApi.Models;
using Supabase;
using Tennis.Data.Services;
using Tennis.Repositories.Command;
using Tennis.Repositories.Queries;
using Tennis.Services;
using Tennis.Strategies;

namespace Tennis.Dependencies
{
    public class TennisDependencies(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureService(IServiceCollection services)
        {

            services.AddControllers();

            SupabaseService supabaseService = ConfigureDbSupaBase();

            services.AddSingleton(supabaseService);

            // Add services to the container.
            services.AddScoped<IReadOnlyRepository, ReadOnlyRepository>();
            services.AddScoped<IWriteRepository, WriteRepository>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IPlayMatchStrategy, MalePlayMatchStrategy>();
            services.AddScoped<IPlayMatchStrategy, FemalePlayMatchStrategy>();

            // Register Mapster
            services.AddMapster();
            services.AddMvc();

            // Configure Mapster
            MapsterDependencies.AddMappingProfile(services);

            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tennis API", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        private static SupabaseService ConfigureDbSupaBase()
        {
            var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
            var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

            var options = new SupabaseOptions { AutoConnectRealtime = true, AutoRefreshToken = true };

            var supabaseService = new SupabaseService(url, key, options);
            supabaseService.InitializeAsync().Wait();
            return supabaseService;
        }
    }
}
