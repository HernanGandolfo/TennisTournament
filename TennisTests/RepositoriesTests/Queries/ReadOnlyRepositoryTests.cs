using Moq;
using Supabase;
using Tennis.Data.Entities;
using Tennis.Data.Services;
using Tennis.Repositories.Queries;
using Tennis.Services.Request;

public class ReadOnlyRepositoryTests
{
    private readonly Mock<SupabaseService> _mockSupabaseService;
    private readonly ReadOnlyRepository _repository;
    private string url = "https://test.supabase.co";
    private string key = "test_key";

    SupabaseOptions options = new SupabaseOptions { AutoConnectRealtime = true, AutoRefreshToken = true };

    public ReadOnlyRepositoryTests()
    {
        _mockSupabaseService = new Mock<SupabaseService>(url, key, options);
        _repository = new ReadOnlyRepository(_mockSupabaseService.Object);
    }

    [Fact]
    public async Task GetPlayersAsync_ShouldReturnPlayers_WhenPredicateIsNull()
    {
        // Arrange
        var players = new List<Player> { new Player { Id = 1, Name = "Player1" } };
        _mockSupabaseService.Setup(s => s.InitializeAsync()).Returns(Task.CompletedTask);
        _mockSupabaseService.Setup(s => s.GetPlayersAsync(null)).ReturnsAsync(players);

        // Act
        var result = await _repository.GetPlayersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Player1", result[0].Name);
    }

    [Fact]
    public async Task GetHistoryTournamentsAsync_ShouldReturnTournaments_WhenRequestIsValid()
    {
        // Arrange
        var request = new TournamentSearchRequest { NameTournament = "Tournament1" };
        var tournaments = new List<Tournament> { new Tournament { Id = 1, Name = "Tournament1" } };
        _mockSupabaseService.Setup(s => s.GetHistoryTournamentAsync(request)).ReturnsAsync(tournaments);

        // Act
        var result = await _repository.GetHistoryTournamentsAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Tournament1", result[0].Name);
    }
}