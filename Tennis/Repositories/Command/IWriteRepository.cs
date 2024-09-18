using Tennis.Data.Entities;
using Tennis.Data.Enum;

namespace Tennis.Repositories.Command
{
    public interface IWriteRepository
    {
        Task AddHistoryTournamentAsync(List<PlayerHistory> history);

        Task<Tournament> CreateTournamentAsync(PlayerType typeTournament);
    }
}
