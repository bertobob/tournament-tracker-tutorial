
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tournament_tracker.Models;

namespace tournament_tracker.Configuration
{
    public interface IDataConnection
    {
        Prize CreatePrize(Prize model);
        List<Person> GetAllPersons();
        List<Team> GetAllTeams();
        List<Prize> GetAllPrizes();
        int AddTournament(string tournamentName, decimal fee);
        bool AddTournamentEntries(int tournamentId, int teamId);
        bool AddTournamentPrize(int id, int prizeId);

        int CreateTeam(string teamName);
        bool AddPersonToDB(Person person);
        bool AddTeamMember(int teamID, int personID);

        bool AddPrize(int placeNumber, string placeName, decimal prizeAmount);

        int AddMatchup(int tournamentId, int? teamAId, int? teamBId, int roundNumber);

        bool AddMatchupEntry(int matchupId, int roundNumber, int? winnerId);

        string GetTeamNameById(int id);
        List <Tournament> GetTournaments();
        List<Matchup> GetTournamentMatches(int tournamentId);
        bool UpdateScore(int id, int teamAId, int team1Score, int winnerId);
        bool AddTeamToNextRound(int nextMatchId, int? teamAId, int? teamBId);
    }
}
