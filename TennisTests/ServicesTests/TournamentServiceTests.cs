using Moq;
using Tennis.Data.Entities;
using Tennis.Data.Enum;
using Tennis.MappingProfile.Dtos;
using Tennis.Services;
using Tennis.Services.Request;

public class TournamentServiceTests
{
    private readonly Mock<ITournamentService> _mockTournamentService;

    public TournamentServiceTests()
    {
        _mockTournamentService = new Mock<ITournamentService>();
    }

    [Fact]
    public async Task GetPlayersRoundsAsyns_ReturnsPlayersList()
    {
        // Arrange
        int numberOfRounds = 3;
        PlayerType typeTournament = PlayerType.Man;
        var expectedPlayers = new List<Player> { new Player { Name = "Player1" }, new Player { Name = "Player2" } };

        _mockTournamentService.Setup(s => s.GetPlayersRoundsAsyns(numberOfRounds, typeTournament))
            .ReturnsAsync(expectedPlayers);

        // Act
        var result = await _mockTournamentService.Object.GetPlayersRoundsAsyns(numberOfRounds, typeTournament);

        // Assert
        Assert.Equal(expectedPlayers, result);
    }

    [Fact]
    public async Task SimulateTournament_ReturnsPlayerDto()
    {
        // Arrange
        var players = new List<Player> { new Player { Name = "Player1" }, new Player { Name = "Player2" } };
        string titleTournament = "Tournament Men's";
        int numberOfRounds = 3;
        PlayerType typeTournament = PlayerType.Man;
        var expectedPlayerDto = new PlayerDto { NamePlayer = "Player1" };

        _mockTournamentService.Setup(s => s.SimulateTournament(players, titleTournament, typeTournament, numberOfRounds))
            .ReturnsAsync(expectedPlayerDto);

        // Act
        var result = await _mockTournamentService.Object.SimulateTournament(players, titleTournament, typeTournament, numberOfRounds);

        // Assert
        Assert.Equal(expectedPlayerDto, result);
    }

    [Fact]
    public async Task GetHistoryTournamentAsync_ReturnsTournamentDtoList()
    {
        // Arrange
        var request = new TournamentSearchRequest { NameTournament = "Tournament Men's" };
        var expectedTournaments = new List<TournamentDto> { new TournamentDto { Name = "Tournament Men's" } };

        _mockTournamentService.Setup(s => s.GetHistoryTournamentAsync(request))
            .ReturnsAsync(expectedTournaments);

        // Act
        var result = await _mockTournamentService.Object.GetHistoryTournamentAsync(request);

        // Assert
        Assert.Equal(expectedTournaments, result);
    }
}
