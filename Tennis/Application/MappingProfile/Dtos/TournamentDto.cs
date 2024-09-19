namespace Tennis.Application.MappingProfile.Dtos
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Created { get; set; }
        public int NumberOfRounds { get; set; }
        public List<PlayerHistoryDto> PlayerHistories { get; set; }
    }
}
