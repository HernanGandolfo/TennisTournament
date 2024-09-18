using System.Linq.Expressions;
using Tennis.Data.Entities;
using Tennis.Data.Services;
using Tennis.Services.Request;

namespace Tennis.Repositories.Queries
{
    public class ReadOnlyRepository(SupabaseService supabaseService) : IReadOnlyRepository
    {
        private readonly SupabaseService _supabaseService = supabaseService;

        public async Task<List<Player>> GetPlayersAsync(Expression<Func<Player, bool>> predicate = null)
        {
            return await _supabaseService.GetPlayersAsync(predicate);
        }

        public async Task<List<Tournament>> GetHistoryTournamentsAsync(TournamentSearchRequest request)
        {
            return await _supabaseService.GetHistoryTournamentAsync(request);
        }
    }
}
