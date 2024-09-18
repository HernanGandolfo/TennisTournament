using Mapster;
using Tennis.Data.Entities;
using Tennis.MappingProfile.Dtos;

namespace Tennis.Services
{
    public class MaleTournament //:ITournament
    {
        public PlayerDto SimulateTournament(List<Player> players)
        {
            while (players.Count > 1)
            {
                List<Player> nextRound = new List<Player>();
                for (int i = 0; i < players.Count; i += 2)
                {
                    //nextRound.Add(PlayMatch((MalePlayer)players[i], (MalePlayer)players[i + 1]));
                }
                players = nextRound;
            }
            return players.First().Adapt<PlayerDto>();
        }

      
    }
}
