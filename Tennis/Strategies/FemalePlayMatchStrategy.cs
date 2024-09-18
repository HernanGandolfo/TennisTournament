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
    }
}
