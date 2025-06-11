using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using tournament_tracker.Models;

namespace tournament_tracker.Configuration
{
    public class TextConnector : IDataConnection
    {
        public Prize CreatePrize(Prize model)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetAllPersons()
        {
            return new List<Person>();
        }

        public bool AddPersonToDB(Person person)
        {
            return false;
        }

        public bool AddTeamMember(int teamID, int personID)
        {
            return false;   
        }

        public int CreateTeam(string teamName)
        {
            return 0;
        }

        public List<Team> GetAllTeams()
        {
            return new List<Team>();
        }
        public bool AddTournamentEntries(int tournamentId, int teamId)
        {
            return false;
        }
        public int AddTournament(string tournamentName, decimal fee)
        {
            return -1;
        }

        public bool AddPrize(int placeNumber, string placeName, decimal prizeAmount)
        {
            return false;
        }
        public List<Prize> GetAllPrizes()
        {
            return new List<Prize>();
        }
        public bool AddTournamentPrize(int id, int prizeId)
        {
            return false;
        }

        public int AddMatchup(int tournamentId, int? teamAId, int? teamBId, int roundNumber)
        {
            return -1;
        }

        public bool AddMatchupEntry(int matchupId, int roundNumber, int? winnerId)
        {
            return false;
        }
        
        public string GetTeamNameById(int id)
        {
            return "keinen";
        }

        public bool UpdateScore(int id, int teamAId, int team1Score, int winnerId)
        {
            return false;
        }

        public bool AddTeamToNextRound(int nextMatchId, int? teamAId, int? teamBId)
        {
            return false;
        }
        public List<Tournament> GetTournaments()
        {
            return new List<Tournament>();
        }

        public List<Matchup> GetTournamentMatches(int tournamentId)
        {
            return new List<Matchup>();
        }
    }
}
