using Mapster;
using Tennis.Data.Entities;

namespace Tennis.Strategies
{
    public class FemalePlayMatchStrategy : IPlayMatchStrategy
    {
        public Player PlayMatch(Player player1, Player player2)
        {
            var play1 = player1.Adapt<FemalePlayer>();
            var play2 = player2.Adapt<FemalePlayer>();

            Random rand = new Random();
            int luckFactor1 = rand.Next(0, 100);
            int luckFactor2 = rand.Next(0, 100);

            int score1 = play1.SkillLevel + play1.ReactionTime + luckFactor1;
            int score2 = play2.SkillLevel + play2.ReactionTime + luckFactor2;

            return score1 > score2 ? play1 : play2;
        }

        public List<PlayerHistory> PlayerHistoryMatch(List<Player> players, int idTournament, int roundPosition)
        {
            var play = players.Adapt<List<FemalePlayer>>();
            var playersHistory = play.Adapt<List<PlayerHistory>>();

            playersHistory.ForEach(x =>
            {
                x.IdTournament = idTournament;
                x.PositionRound = roundPosition;
            });

            return playersHistory;
        }
    }
}
