namespace Tennis.MappingProfile.Dtos
{
    public class PlayerHistoryDto
    {
        public  int IdPlayer { get; set; }
        public string PlayerName { get; set; }
        public string SkillLevel { get; set; }
        public string RoundPosition { get; set; }
        public bool Winner { get; set; }
    }
}
