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
                .Map(x => x.IdPlayer , y => y.Id)
                .Map(x => x.NamePlayer, y => y.Name)
                .Map(x => x.SkillLevel, y => y.SkillLevel)
                .Ignore(x => x.TournamentName);
            
            config.NewConfig<Player, ManPlayer>()
                .Map(x => x.Id, y => y.Id)
                .Map(x => x.Name, y => y.Name)
                .Map(x => x.SkillLevel, y => new Random().Next(0, 100))
                .Map(x => x.Strength, y => new Random().Next(0, 100))
                .Map(x => x.MovementSpeed, y => new Random().Next(0, 100));

            config.NewConfig<Player, WomanPlayer>()
                .Map(x => x.Id, y => y.Id)
                .Map(x => x.Name, y => y.Name)
                .Map(x => x.SkillLevel, y => new Random().Next(0, 100))
                .Map(x => x.ReactionTime, y => new Random().Next(0,100));

            config.NewConfig<WomanPlayer, PlayerDto>()
                .Map(x => x.IdPlayer, y => y.Id)
                .Map(x => x.NamePlayer, y => y.Name)
                .Map(x => x.SkillLevel, y => y.SkillLevel)
                .Ignore(x => x.TournamentName);

            config.NewConfig<ManPlayer, PlayerDto>()
               .Map(x => x.IdPlayer, y => y.Id)
               .Map(x => x.NamePlayer, y => y.Name)
               .Map(x => x.SkillLevel, y => y.SkillLevel)
               .Ignore(x => x.TournamentName);
        }
    }
}
