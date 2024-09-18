using System.Linq.Expressions;
using Tennis.Data.Entities;
using Tennis.Data.Services;

namespace Tennis.Repositories
{
    public class PlayerRepository(SupabaseService supabaseService) : IPlayerRepository
    {
        private readonly SupabaseService _supabaseService = supabaseService;

        public async Task<List<Player>> GetPlayersAsync(Expression<Func<Player, bool>> predicate = null)
        {
            return await _supabaseService.GetPlayersAsync(predicate);
        }
    }
}
