using Tennis.Data.Entities;
using Tennis.Data.Services;

namespace Tennis.Repositories.Command
{
    public class WriteRepository(SupabaseService supabaseService) : IWriteRepository
    {
        private readonly SupabaseService _supabaseService = supabaseService;

        public async Task<HistoryTournament> AddPlayerAsync(HistoryTournament history)
        {
            return await _supabaseService.AddHistoryTournamentAsync(history);
        }
    }
}
