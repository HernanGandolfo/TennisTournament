using Swashbuckle.AspNetCore.Annotations;
using Tennis.Application.Services.Utils;
using Tennis.Core.Enum;

namespace Tennis.Application.Services.Request
{
    public class TournamentSearchRequest
    {
        public string NameTournament { get; set; }

        [ValidateDateTime]
        [SwaggerParameter(Description = "Format search 'YYYY-MM-DD' ")]
        public DateTime? DateTournament { get; set; }

        [SwaggerParameter(Description = "Tournament type: 1 is Men's and 2 is Women's")]
        public PlayerType TypeTournament { get; set; }

        public string NamePlayer { get; set; }

        public bool? Winner { get; set; }
    }
}
