using Tennis.Data.Entities;

namespace Tennis.Strategies
{
    public interface IPlayMatchStrategy
    {
        Player PlayMatch(Player player1, Player player2);
    }
}
