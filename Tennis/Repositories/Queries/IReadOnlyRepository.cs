using System.Linq.Expressions;
using Tennis.Data.Entities;

namespace Tennis.Repositories.Queries
{
    public interface IReadOnlyRepository
    {
        Task<List<Player>> GetPlayersAsync(Expression<Func<Player, bool>> predicate = null);

        Task<List<HistoryTournament>> GetHistoryTournamentsAsync(Expression<Func<HistoryTournament, bool>> predicate = null);
    }
}