using Supabase;
using Supabase.Postgrest.Responses;
using System.Linq.Expressions;
using Tennis.Data.Entities;
using Tennis.Data.Enum;
using Tennis.Services.Request;

namespace Tennis.Data.Services
{
    public class SupabaseService
    {
        private readonly Client _client;

        public SupabaseService(string url, string apiKey, SupabaseOptions options)
        {
            _client = new Client(url, apiKey, options);
        }

        public async Task InitializeAsync()
        {
            await _client.InitializeAsync();
        }

        public async Task<List<Player>> GetPlayersAsync(Expression<Func<Player, bool>> predicate = null)
        {
            if (predicate is null)
            {
                var response = await _client.From<Player>().Get();
                return response.Models;
            }
            else
            {
                var responseWithPredicate = await _client.From<Player>().Where(predicate).Get();
                return responseWithPredicate.Models;
            }
        }

        public async Task<List<Tournament>> GetHistoryTournamentAsync(TournamentSearchRequest request = null)
        {
            ModeledResponse<Tournament> responseNameTour = null;
            List<Tournament> response = null;

            responseNameTour = !string.IsNullOrEmpty(request.NameTournament) ?
                responseNameTour = await _client.From<Tournament>().Filter(x => x.Name, Supabase.Postgrest.Constants.Operator.ILike, $"%{request.NameTournament}%").Get() 
                : responseNameTour = await _client.From<Tournament>().Get();
                
            response = responseNameTour.Models;
            
            if (request.DateTournament.HasValue)
            {
                response = responseNameTour.Models.Where(x => x.Created == request.DateTournament.Value).ToList();
            }

            if (request.TypeTournament != 0)
            {
                response = response.Where(x => x.Type == (int)request.TypeTournament).ToList();
            }
            return response;
        }


        public async Task<bool> AddHistoryTournamentAsync(List<PlayerHistory> history)
        {
            var insertResponse = await _client.From<PlayerHistory>().Insert(history);
            return insertResponse.ResponseMessage.IsSuccessStatusCode;
        }

        public async Task<Tournament> CreateTournamentAsync(PlayerType typeTournament, int numberOfRounds)
        {
            var tournament = new Tournament();
            tournament.Created = DateTime.Now;
            if (typeTournament == PlayerType.Male)
            {
                var existingTournaments = await _client.From<Tournament>().Where(t => t.Type == (int)PlayerType.Male).Get();
                var tournamentNames = existingTournaments.Models.Select(t => t.Name).ToList();
                var newTournamentName = GenerateUniqueTournamentName(tournamentNames, "Tournament Male");

                tournament.Name = newTournamentName;
                tournament.NumberOfRounds = numberOfRounds;
                tournament.Type = (int)PlayerType.Male;
            }
            if (typeTournament == PlayerType.Female)
            {
                var existingTournaments = await _client.From<Tournament>().Where(t => t.Type == (int)PlayerType.Female).Get();
                var tournamentNames = existingTournaments.Models.Select(t => t.Name).ToList();
                var newTournamentName = GenerateUniqueTournamentName(tournamentNames, "Tournament Female");

                tournament.Name = newTournamentName;
                tournament.NumberOfRounds = numberOfRounds;
                tournament.Type = (int)PlayerType.Female;
            }

            return await AddHistoryTournamentAsync(tournament);
        }

        private async Task<Tournament> AddHistoryTournamentAsync(Tournament history)
        {
            var insertResponse = await _client.From<Tournament>().Insert(history);
            var insertedRecord = insertResponse.Models.FirstOrDefault();
            return insertedRecord;
        }

        private string GenerateUniqueTournamentName(List<string> existingNames, string prefix)
        {
            var uniqueName = prefix;
            var counter = 1;
            while (existingNames.Contains(uniqueName))
            {
                uniqueName = $"{prefix} {counter}";
                counter++;
            }
            return uniqueName.Trim();
        }
    }
}
