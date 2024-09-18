using Mapster;
using System.Xml.Linq;
using Tennis.Data.Entities;
using Tennis.Data.Enum;
using Tennis.MappingProfile.Dtos;
using Tennis.Repositories.Command;
using Tennis.Repositories.Queries;
using Tennis.Strategies;

namespace Tennis.Services
{
    public class TournamentService(IReadOnlyRepository readOnlyRepository, IWriteRepository writeRepository, IPlayMatchStrategy playMatchStrategy) : ITournamentService
    {
        private readonly IReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        private readonly IWriteRepository _writeRepository = writeRepository;
        private readonly IPlayMatchStrategy _playMatchStrategy = playMatchStrategy;

        public async Task<Tournament> GetHistoryTournamentAsync()
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

        public async Task<PlayerDto> SimulateTournament(List<Player> players, PlayerType typeTournament)
        {
            IPlayMatchStrategy playMatchStrategy = typeTournament == PlayerType.Male ? new MalePlayMatchStrategy() : new FemalePlayMatchStrategy();

            var createTournament = await _writeRepository.CreateTournamentAsync(typeTournament);
            List<PlayerHistory> histoyOrigins = [];

            int round = 1;
            while (players.Count > 1)
            {
                List<Player> nextRound = [];
                for (int i = 0; i < players.Count; i += 2)
                {
                    var nextPlayer = playMatchStrategy.PlayMatch(players[i], players[i + 1]);
                    nextRound.Add(nextPlayer);
                }
                players = nextRound;
                validatePosition(histoyOrigins, playMatchStrategy.PlayerHistoryMatch(players, createTournament.Id, round++));
            }
            return await WinnerIs(players, createTournament, histoyOrigins);
        }

        private async Task<PlayerDto> WinnerIs(List<Player> players,Tournament tournament, List<PlayerHistory> playerHistories)
        {
            var result = players.First().Adapt<PlayerDto>();
            result.TournamentName = tournament.Name;
            
            playerHistories.ForEach(x =>
            {
                if (x.IdPlayer == result.IdPlayer)
                {
                    x.Winner = true;
                }
            });
            await SaveHistoryAsync(playerHistories);
            return result;
        }

        private void validatePosition(List<PlayerHistory> playerHistoriesOrigin, List<PlayerHistory> playerHistories)
        {
            foreach (var item in playerHistories)
            {
                if (playerHistoriesOrigin.Exists(x=> x.IdPlayer == item.IdPlayer))
                {
                    var playerHistory = playerHistoriesOrigin.FirstOrDefault(x => x.IdPlayer == item.IdPlayer);
                    playerHistory.PositionRound = item.PositionRound;
                }
                else
                {
                    playerHistoriesOrigin.Add(item);
                    
                }
            }
        }

        private async Task SaveHistoryAsync(List<PlayerHistory> history)
        {
            await _writeRepository.AddHistoryTournamentAsync(history);
        }

        private List<Player> ObtenerObjetosAleatorios(List<Player> lista, int cantidad)
        {
            return lista.OrderBy(x => new Random().Next()).Take(cantidad).ToList();
        }
    }
}
