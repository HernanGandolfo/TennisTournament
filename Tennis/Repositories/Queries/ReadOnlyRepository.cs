using System.Linq.Expressions;
using Tennis.Data.Entities;
using Tennis.Data.Services;

namespace Tennis.Repositories.Queries
{
    public class ReadOnlyRepository(SupabaseService supabaseService) : IReadOnlyRepository
    {
        private readonly SupabaseService _supabaseService = supabaseService;

        public async Task<List<Player>> GetPlayersAsync(Expression<Func<Player, bool>> predicate = null)
        {
            return await _supabaseService.GetPlayersAsync(predicate);
        }

        public async Task<List<HistoryTournament>> GetHistoryTournamentsAsync(Expression<Func<HistoryTournament, bool>> predicate = null)
        {
            return await _supabaseService.GetHistoryTournamentAsync(predicate);
        }
    }
}
