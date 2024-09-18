using System.Linq.Expressions;
using Tennis.Data.Entities;
using Tennis.Services.Request;

namespace Tennis.Repositories.Queries
{
    public interface IReadOnlyRepository
    {
        Task<List<Player>> GetPlayersAsync(Expression<Func<Player, bool>> predicate = null);

        Task<List<Tournament>> GetHistoryTournamentsAsync(TournamentSearchRequest request);
    }
}