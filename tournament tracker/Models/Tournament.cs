using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tournament_tracker.Models
{
    public class Tournament
    {
        public string TournamentName { get; set; }
        public double EntryFee { get; set; }
        public List<Team> EnteredTeams { get; set; }=new List<Team>();  
        public List<Prize> Prizes { get; set; }
        public List<List<Matchup>> Rounds { get; set; }


    }
}
