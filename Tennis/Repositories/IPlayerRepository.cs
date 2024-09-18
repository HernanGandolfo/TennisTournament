using System.Linq.Expressions;
using Tennis.Data.Entities;

namespace Tennis.Repositories
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetPlayersAsync(Expression<Func<Player, bool>> predicate = null);
    }
}