using System.Linq.Expressions;
using Tennis.Application.Services.Request;
using Tennis.Core.Entities;

namespace Tennis.Infrastructure.Repositories.Queries
{
    public interface IReadOnlyRepository
    {
        Task<List<Player>> GetPlayersAsync(Expression<Func<Player, bool>> predicate = null);

        Task<List<Tournament>> GetHistoryTournamentsAsync(TournamentSearchRequest request);
    }
}