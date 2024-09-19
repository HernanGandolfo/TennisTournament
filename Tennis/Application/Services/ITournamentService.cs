using Tennis.Application.MappingProfile.Dtos;
using Tennis.Application.Services.Request;
using Tennis.Core.Entities;
using Tennis.Core.Enum;

namespace Tennis.Application.Services
{
    public interface ITournamentService
    {
        Task<List<Player>> GetPlayersRoundsAsyns(int numberOfRounds, PlayerType typeTournament);
        Task<PlayerDto> SimulateTournament(List<Player> players, string titleTournament, PlayerType typeTournament, int numberOfRounds);
        Task<List<TournamentDto>> GetHistoryTournamentAsync(TournamentSearchRequest request);
    }
}
