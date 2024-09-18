using Tennis.Data.Entities;
using Tennis.Data.Enum;
using Tennis.MappingProfile.Dtos;
using Tennis.Services.Request;

namespace Tennis.Services
{
    public interface ITournamentService
    {
        Task<List<Player>> GetPlayersRoundsAsyns(int numberOfRounds, PlayerType typeTournament);
        Task<PlayerDto> SimulateTournament(List<Player> players, PlayerType typeTournament, int numberOfRounds);
        Task<List<TournamentDto>> GetHistoryTournamentAsync(TournamentSearchRequest request);
    }
}
