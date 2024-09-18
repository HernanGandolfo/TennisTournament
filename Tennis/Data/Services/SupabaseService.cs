using Supabase;
using System.Linq.Expressions;
using Tennis.Data.Entities;
using Tennis.Data.Enum;

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

        public async Task<List<Tournament>> GetHistoryTournamentAsync(Expression<Func<Tournament, bool>> predicate = null)
        {
            if (predicate is null)
            {
                var response = await _client.From<Tournament>().Get();
                return response.Models;
            }
            else
            {
                var responseWithPredicate = await _client.From<Tournament>().Where(predicate).Get();
                return responseWithPredicate.Models;
            }
        }

        public async Task AddHistoryTournamentAsync(List<PlayerHistory> history)
        {
            var insertResponse = await _client.From<PlayerHistory>().Insert(history);
            var insertedRecord = insertResponse.Models.FirstOrDefault();
        }

        internal async Task<Tournament> CreateTournamentAsync(PlayerType typeTournament)
        {
            var tournament = new Tournament();
            tournament.Created = DateTime.Now;
            if (typeTournament == PlayerType.Male)
            {
                var existingTournaments = await _client.From<Tournament>().Where(t => t.Type == (int)PlayerType.Male).Get();
                var tournamentNames = existingTournaments.Models.Select(t => t.Name).ToList();
                var newTournamentName = GenerateUniqueTournamentName(tournamentNames, "Tournament Male  ");

                tournament.Name = newTournamentName;
                tournament.Type = (int)PlayerType.Male;
            }
            if (typeTournament == PlayerType.Female)
            {
                var existingTournaments = await _client.From<Tournament>().Where(t => t.Type == (int)PlayerType.Female).Get();
                var tournamentNames = existingTournaments.Models.Select(t => t.Name).ToList();
                var newTournamentName = GenerateUniqueTournamentName(tournamentNames, "Tournament Female  ");

                tournament.Name = newTournamentName;
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
                uniqueName = $"{prefix}{counter}";
                counter++;
            }
            return uniqueName;
        }
    }
}
