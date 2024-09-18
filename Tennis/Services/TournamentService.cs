using Mapster;
using Tennis.Data.Entities;
using Tennis.Data.Enum;
using Tennis.MappingProfile.Dtos;
using Tennis.Repositories.Queries;
using Tennis.Strategies;

namespace Tennis.Services
{
    public class TournamentService(IReadOnlyRepository readOnlyRepository, IPlayMatchStrategy playMatchStrategy) : ITournamentService
    {
        private readonly IReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        private readonly IPlayMatchStrategy _playMatchStrategy = playMatchStrategy;

        public async Task<HistoryTournament> GetHistoryTournamentAsync()
        {
            var result = await _readOnlyRepository.GetHistoryTournamentsAsync();

            return result.FirstOrDefault();
        }

        public async Task<List<Player>> GetPlayersRoundsAsyns(int numberOfRounds, PlayerType typeTournament)
        {
            var intType = (int)typeTournament;
            var players = await _readOnlyRepository.GetPlayersAsync(x => x.PlayerTypeId == intType);

            var canPlayer = (int)Math.Pow(2, numberOfRounds);
            if (players.Count <= canPlayer)
            {
                return null;
            }
            return this.ObtenerObjetosAleatorios(players, canPlayer);
        }

        public PlayerDto SimulateTournament(List<Player> players, PlayerType typeTournament)
        {
            IPlayMatchStrategy playMatchStrategy = typeTournament == PlayerType.Male ? new MalePlayMatchStrategy() : new FemalePlayMatchStrategy();
            
            List<HistoryTournament> history = [];

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
