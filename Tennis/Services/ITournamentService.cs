﻿using Tennis.Data.Entities;
using Tennis.MappingProfile.Dtos;

namespace Tennis.Services
{
    public interface ITournamentService
    {
        Task<List<Player>> GetPlayersRoundsAsyns(int numberOfRounds, PlayerType typeTournament);
        PlayerDto SimulateTournament(List<Player> players, PlayerType typeTournament);
    }
}
