namespace Tennis.MappingProfile.Dtos
{
    public class PlayerHistoryDto
    {
        public  int IdPlayer { get; set; }
        public string PlayerName { get; set; }
        public int SkillLevel { get; set; }
        public int MovementSpeed { get; set; }
        public int ReactionTime { get; set; }
        public int Strength { get; set; }
        public string RoundPosition { get; set; }
        public bool Winner { get; set; }
    }
}
