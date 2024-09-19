using Tennis.Core.Entities;
using Tennis.Core.Enum;

namespace Tennis.Infrastructure.Repositories.Command
{
    public interface IWriteRepository
    {
        Task<bool> AddHistoryTournamentAsync(List<PlayerHistory> history);

        Task<Tournament> CreateTournamentAsync(PlayerType typeTournament, string titleTournament, int numberOfRounds);
    }
}
