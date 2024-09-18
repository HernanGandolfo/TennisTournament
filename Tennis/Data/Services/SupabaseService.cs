using Supabase;
using System.Linq.Expressions;
using System.Reflection;
using Tennis.Data.Entities;

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

        public async Task<List<HistoryTournament>> GetHistoryTournamentAsync(Expression<Func<HistoryTournament, bool>> predicate = null)
        {
            if (predicate is null)
            {
                var response = await _client.From<HistoryTournament>().Get();
                return response.Models;
            }
            else
            {
                var responseWithPredicate = await _client.From<HistoryTournament>().Where(predicate).Get();
                return responseWithPredicate.Models;
            }
        }

        public async Task<HistoryTournament> AddHistoryTournamentAsync(HistoryTournament history)
        {
            var insertResponse = await _client.From<HistoryTournament>().Insert(history);
            var insertedRecord = insertResponse.Models.FirstOrDefault();
            return insertedRecord;
        }
    }
}
