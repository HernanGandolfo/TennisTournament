using Mapster;
using Supabase;
using Tennis.Data.Services;
using Tennis.Repositories;
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
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IPlayMatchStrategy, MalePlayMatchStrategy>();
            services.AddScoped<IPlayMatchStrategy, FemalePlayMatchStrategy>();

            // Register Mapster
            services.AddMapster();

            // Configure Mapster
            MapsterDependencies.AddMappingProfile(services);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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
