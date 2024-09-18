using Mapster;
using Tennis.Data.Entities;
using Tennis.Data.Enum;
using Tennis.MappingProfile.Dtos;
using Tennis.Repositories.Command;
using Tennis.Repositories.Queries;
using Tennis.Services.Request;
using Tennis.Strategies;

namespace Tennis.Services
{
    public class TournamentService(IReadOnlyRepository readOnlyRepository, IWriteRepository writeRepository, IPlayMatchStrategy playMatchStrategy) : ITournamentService
    {
        private readonly IReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        private readonly IWriteRepository _writeRepository = writeRepository;
        private readonly IPlayMatchStrategy _playMatchStrategy = playMatchStrategy;

        public async Task<List<TournamentDto>> GetHistoryTournamentAsync(TournamentSearchRequest request)
        {
            var result = await _readOnlyRepository.GetHistoryTournamentsAsync(request);

            // Construir una consulta dinámica
            var query = result.AsQueryable();

            if (!string.IsNullOrEmpty(request.NamePlayer))
            {
                query = query.Where(x => x.PlayerHistories.Any(y => y.PlayerName.ToLower().Contains(request.NamePlayer.ToLower())));
            }

            if (request.Winner.HasValue)
            {
                query = query.Where(x => x.PlayerHistories.Any(y => y.Winner == request.Winner.Value));
            }

            // Ejecutar la consulta y traer los datos a memoria
            var tournaments = query.ToList();

            return FilterAndSortTournaments(request, tournaments);
        }

        public async Task<List<Player>> GetPlayersRoundsAsyns(int numberOfRounds, PlayerType typeTournament)
        {
            var intType = (int)typeTournament;
            var players = await _readOnlyRepository.GetPlayersAsync(x => x.PlayerTypeId == intType);

            var canPlayer = (int)Math.Pow(2, numberOfRounds);

            return (players.Count <= canPlayer || canPlayer < 0) ? null : this.ObtenerObjetosAleatorios(players, canPlayer);
        }

        public async Task<PlayerDto> SimulateTournament(List<Player> players, string titleTournament, PlayerType typeTournament, int numberOfRounds)
        {
            IPlayMatchStrategy playMatchStrategy = typeTournament == PlayerType.Man ? new ManPlayMatchStrategy() : new WomanPlayMatchStrategy();

            var createTournament = await _writeRepository.CreateTournamentAsync(typeTournament, titleTournament, numberOfRounds);
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
                this.validatePosition(histoyOrigins, playMatchStrategy.PlayerHistoryMatch(players, createTournament.Id, round++));
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
            await this.SaveHistoryAsync(playerHistories);
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

        private async Task SaveHistoryAsync(List<PlayerHistory> history) => await _writeRepository.AddHistoryTournamentAsync(history);

        private List<Player> ObtenerObjetosAleatorios(List<Player> lista, int cantidad) => lista.OrderBy(x => new Random().Next()).Take(cantidad).ToList();

        private static List<TournamentDto> FilterAndSortTournaments(TournamentSearchRequest request, List<Tournament> tournaments)
        {
            // Filtrar manualmente PlayerHistories para cada torneo
            var filteredTournaments = tournaments.Select(t =>
            {
                if (!string.IsNullOrEmpty(request.NamePlayer))
                {
                    t.PlayerHistories = t.PlayerHistories
                        .Where(y => y.PlayerName.ToLower().Contains(request.NamePlayer.ToLower()))
                        .ToList();
                }

                if (request.Winner.HasValue)
                {
                    t.PlayerHistories = t.PlayerHistories
                        .Where(y => y.Winner == request.Winner.Value)
                        .ToList();
                }

                // Ordenar PlayerHistories por PositionRound
                t.PlayerHistories = t.PlayerHistories
                    .OrderByDescending(ph => ph.PositionRound)
                    .ToList();

                return t;
            }).ToList();

            // Mapear a DTO
            return filteredTournaments.Adapt<List<TournamentDto>>();
        }

    }
}
