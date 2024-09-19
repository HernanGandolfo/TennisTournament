using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Reflection.Metadata;

namespace Tennis.Core.Entities
{
    [Table("tournament")]
    public class Tournament : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public int Type { get; set; }

        [Column("numberOfRounds")]
        public int NumberOfRounds { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Reference(typeof(PlayerHistory))]
        public List<PlayerHistory> PlayerHistories { get; set; }
    }
}
