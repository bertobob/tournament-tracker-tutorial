using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tournament_tracker.Configuration;

namespace tournament_tracker.Models
{
    public class Tournament
    {
        public string TournamentName { get; set; }
        public decimal EntryFee { get; set; }
        public List<Team> EnteredTeams { get; set; }=new List<Team>();  
        public List<Prize> Prizes { get; set; }
        public List<Matchup> Matches { get; set; }

        public int RoundNumbers { get; set; }
        public int id { get; set; }
        public Tournament()
        {

        }

        public Tournament(int id, string tournamentName, decimal entryFee)
        {
            TournamentName = tournamentName;
            EntryFee = entryFee;
            this.id = id;
            //Matches = new List<Matchup>();
            Matches= GlobalConfig.Connections[0].GetTournamentMatches(this.id);
        }

        public void GetMatches()
        {
            this.Matches=GlobalConfig.Connections[0].GetTournamentMatches(this.id);
        }
    }
}
