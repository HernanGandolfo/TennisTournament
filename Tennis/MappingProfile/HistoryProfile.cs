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
            config.NewConfig<MalePlayer, PlayerHistory>()
               .Map(x => x.IdPlayer, y => y.Id)
               .Map(x => x.PlayerName, y => y.Name)
               .Map(x => x.SkillLevel, y => y.SkillLevel)
               .Map(x => x.Strength, y => y.Strength)
               .Map(x => x.MovementSpeed, y => y.MovementSpeed)
               .Ignore(x => x.Id, x => x.ReactionTime, x => x.Winner);

            config.NewConfig<FemalePlayer, PlayerHistory>()
              .Map(x => x.IdPlayer, y => y.Id)
              .Map(x => x.PlayerName, y => y.Name)
              .Map(x => x.SkillLevel, y => y.SkillLevel)
              .Map(x => x.ReactionTime, y => y.ReactionTime)
              .Ignore(x => x.Id, x => x.MovementSpeed,x => x.Strength, x => x.Winner);

        }
    }
}
