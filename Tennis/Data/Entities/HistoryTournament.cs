using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Reflection.Metadata;

namespace Tennis.Data.Entities
{
    [Table("historyTournament")]
    public class HistoryTournament : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }
        
        [Column("nameTornament")]
        public string NameTornament { get; set; }
        
        [Column("idPlayer")]
        public int IdPlayer { get; set; }
        
        [Column("playerName")]
        public int PlayerName { get; set; }

        [Column("skillLevel")]
        public int SkillLevel { get; set; }

        [Column("positionRound")]
        public int PositionRound { get; set; }

        [Column("tournamentDate")]
        public DateTime TournamentDate { get; set; }
    }
}
