using Tennis.Data.Entities;
using Tennis.Strategies;

public class ManPlayMatchStrategyTests
{
    private readonly ManPlayMatchStrategy _manPlayMatchStrategy;

    public ManPlayMatchStrategyTests()
    {
        _manPlayMatchStrategy = new ManPlayMatchStrategy();
    }

    [Fact]
    public void PlayMatch_ReturnsWinner()
    {
        var player1 = new Player { Name = "Player1", SkillLevel = 5 };
        var player2 = new Player { Name = "Player2", SkillLevel = 3 };

        var result = _manPlayMatchStrategy.PlayMatch(player1, player2);

        Assert.Equal(player1.Name, result.Name);
    }

    [Fact]
    public void PlayMatch_ReturnsWinner_WhenSkillLevelsAreEqual()
    {
        var player1 = new Player { Name = "Player1", SkillLevel = 5 };
        var player2 = new Player { Name = "Player2", SkillLevel = 5 };

        var result = _manPlayMatchStrategy.PlayMatch(player1, player2);

        Assert.NotNull(result);
    }

    [Fact]
    public void PlayMatch_ThrowsException_WhenPlayerIsNull()
    {
        var player1 = new Player { Name = "Player1", SkillLevel = 5 };

        Assert.Throws<ArgumentNullException>(() => _manPlayMatchStrategy.PlayMatch(player1, null));
        Assert.Throws<ArgumentNullException>(() => _manPlayMatchStrategy.PlayMatch(null, player1));
    }
}