using Tennis.Core.Entities;

namespace Tennis.Core.Strategies
{
    public interface IPlayMatchStrategy
    {
        Player PlayMatch(Player player1, Player player2);
        List<PlayerHistory> PlayerHistoryMatch(List<Player> players, int idTournament, int roundPosition);
    }
}
