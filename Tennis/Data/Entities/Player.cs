using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Tennis.Data.Entities
{
    [Table("players")]
    public class Player : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("playerTypeId")]
        public int PlayerTypeId { get; set; }


        public int SkillLevel { get; set; }
    }
}
