
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
