using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Tennis.Core.Enum;
using Tennis.Application.Services;
using Tennis.Application.Services.Request;
using Tennis.Application.Services.Utils;
using Tennis.Application.MappingProfile.Dtos;

namespace Tennis.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class TournamentController(ITournamentService tournament) : ControllerBase
    {
        private readonly ITournamentService _tournament = tournament;

        [HttpGet("simulate")]
        [SwaggerOperation(Summary = "Simulate a tournament and return the winner.")]
        [ProducesDefaultResponseType(typeof(PlayerDto))]
        public async Task<IActionResult> SimulateTournament(
            [SwaggerParameter(Description = "Type: 1 default title is Tournament Men's" +
                " Type: 2 default title is Tournament is Women's"), FromQuery] string titleTournament,
            [Required, FromQuery] int numberOfRounds,
            [SwaggerParameter(Description = "Tournament type: 1 is Men's and 2 is Women's"), FromQuery] PlayerType typeTournament)
        {
            var canPlayers = await _tournament.GetPlayersRoundsAsyns(numberOfRounds, typeTournament);

            if (canPlayers is null)
            {
                return BadRequest(
                    ProblemDetailsHelper.CreateProblemDetails(
                        HttpContext,
                        ConstansErrorMessage.ErrorNumberPlayer,
                        (int)HttpStatusCode.BadRequest));
            }

            PlayerDto winner = await _tournament.SimulateTournament(canPlayers, titleTournament, typeTournament, numberOfRounds);
            return Ok(winner);
        }

        [HttpGet("historyTournament")]
        [SwaggerOperation(Summary = "Returns tournament history.")]
        [ProducesDefaultResponseType(typeof(List<PlayerHistoryDto>))]
        public async Task<IActionResult> HistoryTournament([FromQuery] TournamentSearchRequest request)
        {
            var result = await _tournament.GetHistoryTournamentAsync(request);
            return Ok(result);
        }
    }
}