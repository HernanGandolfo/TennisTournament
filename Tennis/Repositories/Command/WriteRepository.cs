using Tennis.Data.Entities;
using Tennis.Data.Enum;
using Tennis.Data.Services;

namespace Tennis.Repositories.Command
{
    public class WriteRepository(SupabaseService supabaseService) : IWriteRepository
    {
        private readonly SupabaseService _supabaseService = supabaseService;

        public async Task<bool> AddHistoryTournamentAsync(List<PlayerHistory> history)
        {
            return await _supabaseService.AddHistoryTournamentAsync(history);
        }

        public async Task<Tournament> CreateTournamentAsync(PlayerType typeTournament, string titleTournament, int numberOfRounds)
        {
            return await _supabaseService.CreateTournamentAsync(typeTournament, titleTournament, numberOfRounds);
        }
    }
}
