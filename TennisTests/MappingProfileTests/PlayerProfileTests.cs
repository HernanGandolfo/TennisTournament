using Mapster;
using Tennis.Application.MappingProfile;
using Tennis.Application.MappingProfile.Dtos;
using Tennis.Core.Entities;

namespace Tennis.Tests.MappingProfile
{
    public class PlayerProfileTests
    {
        private readonly TypeAdapterConfig _config;

        public PlayerProfileTests()
        {
            _config = new TypeAdapterConfig();
            new PlayerProfile().Register(_config);
        }

        [Fact]
        public void PlayerToPlayerDtoMapping_ShouldBeValid()
        {
            var player = new Player { Id = 1, Name = "John Doe", SkillLevel = 5 };

            var playerDto = player.Adapt<PlayerDto>(_config);

            Assert.Equal(player.Id, playerDto.IdPlayer);
            Assert.Equal(player.Name, playerDto.NamePlayer);
            Assert.Equal(player.SkillLevel, playerDto.SkillLevel);
        }

        [Fact]
        public void PlayerToManPlayerMapping_ShouldBeValid()
        {
            var player = new Player { Id = 1, Name = "John Doe", SkillLevel = 5 };

            var manPlayer = player.Adapt<ManPlayer>(_config);

            Assert.Equal(player.Id, manPlayer.Id);
            Assert.Equal(player.Name, manPlayer.Name);
            Assert.InRange(manPlayer.SkillLevel, 0, 100);
            Assert.InRange(manPlayer.Strength, 0, 100);
            Assert.InRange(manPlayer.MovementSpeed, 0, 100);
        }

        [Fact]
        public void PlayerToWomanPlayerMapping_ShouldBeValid()
        {
            var player = new Player { Id = 1, Name = "Jane Doe", SkillLevel = 5 };

            var womanPlayer = player.Adapt<WomanPlayer>(_config);

            Assert.Equal(player.Id, womanPlayer.Id);
            Assert.Equal(player.Name, womanPlayer.Name);
            Assert.InRange(womanPlayer.SkillLevel, 0, 100);
            Assert.InRange(womanPlayer.ReactionTime, 0, 100);
        }

        [Fact]
        public void WomanPlayerToPlayerDtoMapping_ShouldBeValid()
        {
            var womanPlayer = new WomanPlayer { Id = 1, Name = "Jane Doe", SkillLevel = 5 };

            var playerDto = womanPlayer.Adapt<PlayerDto>(_config);

            Assert.Equal(womanPlayer.Id, playerDto.IdPlayer);
            Assert.Equal(womanPlayer.Name, playerDto.NamePlayer);
            Assert.Equal(womanPlayer.SkillLevel, playerDto.SkillLevel);
        }

        [Fact]
        public void ManPlayerToPlayerDtoMapping_ShouldBeValid()
        {
            var manPlayer = new ManPlayer { Id = 1, Name = "John Doe", SkillLevel = 5 };

            var playerDto = manPlayer.Adapt<PlayerDto>(_config);

            Assert.Equal(manPlayer.Id, playerDto.IdPlayer);
            Assert.Equal(manPlayer.Name, playerDto.NamePlayer);
            Assert.Equal(manPlayer.SkillLevel, playerDto.SkillLevel);
        }
    }
}