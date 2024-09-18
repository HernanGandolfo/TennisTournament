using Mapster;
using Microsoft.AspNetCore.Identity.Data;
using Tennis.Data.Entities;
using Tennis.MappingProfile.Dtos;

namespace Tennis.Strategies
{
    public class MalePlayMatchStrategy : IPlayMatchStrategy
    {
        public Player PlayMatch(Player player1, Player player2)
        {
            var play1 = player1.Adapt<MalePlayer>();
            var play2 = player2.Adapt<MalePlayer>();

            Random rand = new();
            int luckFactor1 = rand.Next(0, 100);
            int luckFactor2 = rand.Next(0, 100);

            int score1 = play1.SkillLevel + play1.Strength + play1.MovementSpeed + luckFactor1;
            int score2 = play2.SkillLevel + play2.Strength + play2.MovementSpeed + luckFactor2;

            return score1 > score2 ? play1 : play2;
        }

        public List<PlayerHistory> PlayerHistoryMatch(List<Player> players, int idTournament, int roundPosition)
        {
            var play = players.Adapt<List<MalePlayer>>();
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
