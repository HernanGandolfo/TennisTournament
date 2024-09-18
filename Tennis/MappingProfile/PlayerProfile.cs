using Mapster;
using Tennis.Data.Entities;
using Tennis.MappingProfile.Dtos;

namespace Tennis.MappingProfile
{
    public class PlayerProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Player, PlayerDto>()
                .Map(x => x.Id , y => y.Id)
                .Map(x => x.Name, y => y.Name)
                .Map(x => x.SkillLevel, y => y.SkillLevel);

            config.NewConfig<Player, FemalePlayer>()
                .Map(x => x.Id, y => y.Id)
                .Map(x => x.Name, y => y.Name)
                .Map(x => x.SkillLevel, y => y.SkillLevel)
                .Map(x => x.ReactionTime, y => new Random().Next(0,100));
        }
    }
}
