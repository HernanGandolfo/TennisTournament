using Tennis.Data.Entities;
using Tennis.Strategies;

public class WomanPlayMatchStrategyTests
{
    private readonly WomanPlayMatchStrategy _womanPlayMatchStrategy;

    public WomanPlayMatchStrategyTests()
    {
        _womanPlayMatchStrategy = new WomanPlayMatchStrategy();
    }

    [Fact]
    public void PlayMatch_ReturnsWinner()
    {
        var player1 = new WomanPlayer { Name = "Player1", SkillLevel = 5 };
        var player2 = new WomanPlayer { Name = "Player2", SkillLevel = 3 };

        var result = _womanPlayMatchStrategy.PlayMatch(player1, player2);

        Assert.Equal(player1.Name, result.Name);
    }

    [Fact]
    public void PlayMatch_ReturnsWinner_WhenSkillLevelsAreEqual()
    {
        var player1 = new WomanPlayer { Name = "Player1", SkillLevel = 5 };
        var player2 = new WomanPlayer { Name = "Player2", SkillLevel = 5 };

        var result = _womanPlayMatchStrategy.PlayMatch(player1, player2);

        Assert.NotNull(result);
    }

    [Fact]
    public void PlayMatch_ThrowsException_WhenPlayerIsNull()
    {
        var player1 = new WomanPlayer { Name = "Player1", SkillLevel = 5 };

        Assert.Throws<ArgumentNullException>(() => _womanPlayMatchStrategy.PlayMatch(player1, null));
        Assert.Throws<ArgumentNullException>(() => _womanPlayMatchStrategy.PlayMatch(null, player1));
    }

    [Fact]
    public void PlayerHistoryMatch_ValidPlayers_ReturnsCorrectHistory()
    {
        var players = new List<Player>
        {
            new WomanPlayer { Id = 1, Name = "Player1" },
            new WomanPlayer { Id = 2, Name = "Player2" }
        };
        int idTournament = 1;
        int roundPosition = 2;

        var result = _womanPlayMatchStrategy.PlayerHistoryMatch(players, idTournament, roundPosition);

        Assert.Equal(2, result.Count);
        Assert.All(result, history =>
        {
            Assert.Equal(idTournament, history.IdTournament);
            Assert.Equal(roundPosition, history.PositionRound);
        });
    }
}
