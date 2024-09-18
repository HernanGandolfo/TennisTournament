using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using Tennis.MappingProfile.Dtos;
using System.Globalization;
using Tennis.Services;
using Tennis.Data.Enum;
using Tennis.Services.Request;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [CustomFilterException]
    public class TournamentController(ITournamentService tournament) : ControllerBase
    {
        private readonly ITournamentService _tournament = tournament;

        [HttpGet("simulate")]
        [SwaggerOperation(Summary = "Simulate a tournament and return the winner.")]
        [ProducesDefaultResponseType(typeof(PlayerDto))]
        public async Task<IActionResult> SimulateTournament(
            [Required, FromQuery] int numberOfRounds,
            [SwaggerParameter(Description = "Tournament type: 1 is Men's and 2 is Women's"), FromQuery] PlayerType typeTournament)
        {
            var canPlayers = await _tournament.GetPlayersRoundsAsyns(numberOfRounds, typeTournament);

            if (canPlayers is null)
            {
                return BadRequest( new ValidationResult("The number of players does not match the number of rounds specified."));
            }

            PlayerDto winner = await _tournament.SimulateTournament(canPlayers, typeTournament);
            return Ok(winner);
        }

        [HttpGet("historyTournament")]
        [SwaggerOperation(Summary = "Returns tournament history.")]
        [ProducesDefaultResponseType(typeof(List<PlayerHistoryDto>))]
        public async Task<IActionResult> HistoryTournament([FromQuery] TournamentSearchRequest request)
        {
            return Ok(await _tournament.GetHistoryTournamentAsync());
        }
    }
}