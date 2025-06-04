using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tournament_tracker.Models
{
   public  class Matchup
    {
        public List<MatchupEntry> Entries { get; set; }= new List<MatchupEntry> ();
        public Team Winner {  get; set; }
        public int MatchupRound {  get; set; }
    }
}
