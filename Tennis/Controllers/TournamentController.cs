using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using Tennis.Data.Entities;
using Tennis.MappingProfile.Dtos;
using Tennis.Services;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ProducesResponseType(400)]
    //[ProducesResponseType(401)]
    //[ProducesResponseType(403)]
    //[ProducesResponseType(404)]
    //[ProducesResponseType(500)]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournament;

        public TournamentController(ITournamentService tournament)
        {
            _tournament = tournament;
        }

        [HttpGet("simulate")]
        [SwaggerOperation(Summary = "Simula un torneo y devuelve el ganador.")]
        [ProducesDefaultResponseType(typeof(PlayerDto))]
        public async Task<IActionResult> SimulateTournament(
            [Required, FromQuery] int numberOfRounds,
            [SwaggerParameter(Description = "Tipo de torneo: 1 es Masculino y 2 es Femenino"), FromQuery] PlayerType typeTournament)
        {
            var canPlayers = await _tournament.GetPlayersRoundsAsyns(numberOfRounds, typeTournament);

            if (canPlayers is null)
            {
                return BadRequest("La cantidad de jugadores no coincide con la cantidad de rondas especificadas.");
            }

            PlayerDto winner = _tournament.SimulateTournament(canPlayers, typeTournament);
            return Ok(winner);
        }
    }
}