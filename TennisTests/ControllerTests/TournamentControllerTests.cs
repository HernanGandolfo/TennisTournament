using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tennis.Core.Entities;
using Tennis.Core.Enum;
using Tennis.Application.Services;
using Tennis.Application.MappingProfile.Dtos;
using Tennis.Presentation.Controllers;
using Tennis.Application.Services.Request;

namespace Tennis.Tests
{

    public class TournamentControllerTests
    {
        private readonly Mock<ITournamentService> _mockTournamentService;
        private readonly TournamentController _controller;
        private const List<Player>? ListNull = (List<Player>)null;

        public TournamentControllerTests()
        {
            _mockTournamentService = new Mock<ITournamentService>();
            _controller = new TournamentController(_mockTournamentService.Object);
        }

        [Fact]
        public async Task SimulateTournament_ReturnsOkResult_WithWinner()
        {
            var titleTournament = "Tournament Men's";
            var numberOfRounds = 3;
            var typeTournament = PlayerType.Man;
            var players = new List<Player> { new Player { Name = "Player1" } };
            var winner = new PlayerDto { NamePlayer = "Winner" };

            _mockTournamentService.Setup(s => s.GetPlayersRoundsAsyns(numberOfRounds, typeTournament))
                .ReturnsAsync(players);
            _mockTournamentService.Setup(s => s.SimulateTournament(players, titleTournament, typeTournament, numberOfRounds))
                .ReturnsAsync(winner);

            var result = await _controller.SimulateTournament(titleTournament, numberOfRounds, typeTournament);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PlayerDto>(okResult.Value);
            Assert.Equal(winner.NamePlayer, returnValue.NamePlayer);
        }

        [Fact]
        public async Task SimulateTournament_ReturnsBadRequest_WhenPlayersAreNull()
        {
            var titleTournament = "Tournament Men's";
            var numberOfRounds = 3;
            var typeTournament = PlayerType.Man;

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(x => x.Request.Path).Returns(new PathString("/api/Tournament/simulate"));
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            _mockTournamentService.Setup(s => s.GetPlayersRoundsAsyns(numberOfRounds, typeTournament)).ReturnsAsync(ListNull);
            var result = await _controller.SimulateTournament(titleTournament, numberOfRounds, typeTournament);

            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task HistoryTournament_ReturnsOkResult_WithHistory()
        {
            var request = new TournamentSearchRequest();

            var history = new List<PlayerHistoryDto> { new PlayerHistoryDto { PlayerName = "Player1" } };
            var torunaments = new List<TournamentDto> { new TournamentDto { Name = "Tournament1", PlayerHistories = history } };

            _mockTournamentService.Setup(s => s.GetHistoryTournamentAsync(request)).ReturnsAsync(torunaments);

            var result = await _controller.HistoryTournament(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<TournamentDto>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Tournament1", returnValue[0].Name);
        }
    }
}