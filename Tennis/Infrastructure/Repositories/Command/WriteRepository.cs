using Tennis.Core.Entities;
using Tennis.Core.Enum;
using Tennis.Core.Services;

namespace Tennis.Infrastructure.Repositories.Command
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
