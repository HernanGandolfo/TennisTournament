using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Tennis.Core.Entities
{
    [Table("playerHistory")]
    public class PlayerHistory : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("idTournament")]
        public int IdTournament { get; set; }

        [Column("idPlayer")]
        public int IdPlayer { get; set; }

        [Column("playerName")]
        public string PlayerName { get; set; }

        [Column("movementSpeed")]
        public int MovementSpeed { get; set; }

        [Column("skillLevel")]
        public int SkillLevel { get; set; }

        [Column("reactionTime")]
        public int ReactionTime { get; set; }

        [Column("strength")]
        public int Strength { get; set; }

        [Column("positionRound")]
        public int PositionRound { get; set; }

        [Column("winner")]
        public bool Winner { get; set; } = false;
    }
}
