using Mapster;
using Tennis.Data.Entities;
using Tennis.MappingProfile.Dtos;
using Tennis.Repositories;
using Tennis.Strategies;

namespace Tennis.Services
{
    public class TournamentService(IPlayerRepository playerRepository, IPlayMatchStrategy malePlayMatchStrategy, IPlayMatchStrategy femalePlayMatchStrategy) : ITournamentService
    {
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly IPlayMatchStrategy _malePlayMatchStrategy = malePlayMatchStrategy;
        private readonly IPlayMatchStrategy _femalePlayMatchStrategy = femalePlayMatchStrategy;

        public async Task<List<Player>> GetPlayersRoundsAsyns(int numberOfRounds, PlayerType typeTournament)
        {
            var players = await _playerRepository.GetPlayersAsync(x => x.PlayerTypeId == (int)typeTournament);

            if (players.Count <= numberOfRounds * 2)
            {
                return null;
            }
            return this.ObtenerObjetosAleatorios(players, numberOfRounds * 2);
        }

        public PlayerDto SimulateTournament(List<Player> players, PlayerType typeTournament)
        {
            IPlayMatchStrategy playMatchStrategy = typeTournament == PlayerType.Male ? _malePlayMatchStrategy : _femalePlayMatchStrategy;

            while (players.Count > 1)
            {
                List<Player> nextRound = [];
                for (int i = 0; i < players.Count; i += 2)
                {
                    nextRound.Add(playMatchStrategy.PlayMatch(players[i], players[i + 1]));
                }
                players = nextRound;
            }
            return players.First().Adapt<PlayerDto>();
        }

        private List<Player> ObtenerObjetosAleatorios(List<Player> lista, int cantidad)
        {
            return lista.OrderBy(x => new Random().Next()).Take(cantidad).ToList();
        }
    }
}
