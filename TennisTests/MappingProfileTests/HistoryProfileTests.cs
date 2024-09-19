using Mapster;
using Tennis.Data.Entities;
using Tennis.Data.Enum;
using Tennis.MappingProfile;
using Tennis.MappingProfile.Dtos;
using Xunit;

namespace Tennis.Tests.MappingProfile
{
    public class HistoryProfileTests
    {
        private readonly TypeAdapterConfig _config;

        public HistoryProfileTests()
        {
            _config = new TypeAdapterConfig();
            new HistoryProfile().Register(_config);
        }

        [Fact]
        public void ManPlayerToPlayerHistoryMapping_ShouldBeValid()
        {
            var manPlayer = new ManPlayer { Id = 1, Name = "John Doe", SkillLevel = 5, Strength = 80, MovementSpeed = 70 };

            var playerHistory = manPlayer.Adapt<PlayerHistory>(_config);

            Assert.Equal(manPlayer.Id, playerHistory.IdPlayer);
            Assert.Equal(manPlayer.Name, playerHistory.PlayerName);
            Assert.Equal(manPlayer.SkillLevel, playerHistory.SkillLevel);
            Assert.Equal(manPlayer.Strength, playerHistory.Strength);
            Assert.Equal(manPlayer.MovementSpeed, playerHistory.MovementSpeed);
        }

        [Fact]
        public void WomanPlayerToPlayerHistoryMapping_ShouldBeValid()
        {
            var womanPlayer = new WomanPlayer { Id = 1, Name = "Jane Doe", SkillLevel = 5, ReactionTime = 90 };

            var playerHistory = womanPlayer.Adapt<PlayerHistory>(_config);

            Assert.Equal(womanPlayer.Id, playerHistory.IdPlayer);
            Assert.Equal(womanPlayer.Name, playerHistory.PlayerName);
            Assert.Equal(womanPlayer.SkillLevel, playerHistory.SkillLevel);
            Assert.Equal(womanPlayer.ReactionTime, playerHistory.ReactionTime);
        }

        [Fact]
        public void TournamentToTournamentDtoMapping_ShouldBeValid()
        {
            var tournament = new Tournament
            {
                Id = 1,
                Name = "Grand Slam",
                Created = DateTime.Now,
                NumberOfRounds = 5,
                Type = (int)PlayerType.Man,
                PlayerHistories = new List<PlayerHistory> { new PlayerHistory { IdPlayer = 1, PlayerName = "John Doe" } }
            };

            var tournamentDto = tournament.Adapt<TournamentDto>(_config);

            Assert.Equal(tournament.Id, tournamentDto.Id);
            Assert.Equal(tournament.Name, tournamentDto.Name);
            Assert.Equal(tournament.Created, tournamentDto.Created);
            Assert.Equal(tournament.NumberOfRounds, tournamentDto.NumberOfRounds);
            Assert.Equal(PlayerType.Man.ToString(), tournamentDto.Type);
            Assert.Equal(tournament.PlayerHistories.Count, tournamentDto.PlayerHistories.Count);
        }

        [Fact]
        public void PlayerHistoryToPlayerHistoryDtoMapping_ShouldBeValid()
        {
            var playerHistory = new PlayerHistory
            {
                IdPlayer = 0,
                PlayerName = "John Doe",
                SkillLevel = 5,
                MovementSpeed = 70,
                Strength = 80,
                PositionRound = 3,
                Winner = true
            };

            var playerHistoryDto = playerHistory.Adapt<PlayerHistoryDto>(_config);

            Assert.Equal(playerHistory.IdPlayer, playerHistoryDto.IdPlayer);
            Assert.Equal(playerHistory.PlayerName, playerHistoryDto.PlayerName);
            Assert.Equal(playerHistory.SkillLevel, playerHistoryDto.SkillLevel);
            Assert.Equal(playerHistory.MovementSpeed, playerHistoryDto.MovementSpeed);
            Assert.Equal(playerHistory.Strength, playerHistoryDto.Strength);
            Assert.Equal($"Finalist in the round {playerHistory.PositionRound}", playerHistoryDto.RoundPosition);
            Assert.Equal(playerHistory.Winner, playerHistoryDto.Winner);
        }
    }
}
