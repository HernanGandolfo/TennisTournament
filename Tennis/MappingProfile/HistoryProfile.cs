using Mapster;
using Tennis.Data.Entities;
using Tennis.Data.Enum;
using Tennis.MappingProfile.Dtos;

namespace Tennis.MappingProfile
{
    public class HistoryProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ManPlayer, PlayerHistory>()
               .Map(x => x.IdPlayer, y => y.Id)
               .Map(x => x.PlayerName, y => y.Name)
               .Map(x => x.SkillLevel, y => y.SkillLevel)
               .Map(x => x.Strength, y => y.Strength)
               .Map(x => x.MovementSpeed, y => y.MovementSpeed)
               .Ignore(x => x.Id, x => x.ReactionTime, x => x.Winner);

            config.NewConfig<WomanPlayer, PlayerHistory>()
              .Map(x => x.IdPlayer, y => y.Id)
              .Map(x => x.PlayerName, y => y.Name)
              .Map(x => x.SkillLevel, y => y.SkillLevel)
              .Map(x => x.ReactionTime, y => y.ReactionTime)
              .Ignore(x => x.Id, x => x.MovementSpeed,x => x.Strength, x => x.Winner);

            config.NewConfig<Tournament, TournamentDto>()
                .Map(x => x.Id, y => y.Id)
                .Map(x => x.Name, y => y.Name)
                .Map(x => x.Created, y => y.Created)
                .Map(x => x.NumberOfRounds, y => y.NumberOfRounds)
                .Map(x => x.Type, y => y.Type == 1 ? PlayerType.Man.ToString() : PlayerType.Woman.ToString())
                .Map(x => x.PlayerHistories, y => y.PlayerHistories);

            config.NewConfig<PlayerHistory, PlayerHistoryDto>()
               .Map(x => x.IdPlayer, y => y.IdPlayer)
               .Map(x => x.PlayerName, y => y.PlayerName)
               .Map(x => x.SkillLevel, y => y.SkillLevel)
               .Map(x => x.MovementSpeed, y => y.MovementSpeed)
               .Map(x => x.Strength, y => y.Strength)
               .Map(x => x.MovementSpeed, y => y.MovementSpeed)
               .Map(x => x.RoundPosition, y => $"Finalist in the round {y.PositionRound}")
               .Map(x => x.Winner, y => y.Winner);
        }
    }
}
