using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tournament_tracker.Models
{
    public class MatchupEntry
    {
        public int Id { get; set; } 
        public int MatchupId {  get; set; }
        public int RoundNumber { get; set; }
        public int? WinnerID { get; set; }
        public MatchupEntry(int id, int matchupId, int roundNumber,int? winnerId) 
        {
            Id = id;
            MatchupId = matchupId;  
            RoundNumber = roundNumber;
            WinnerID = winnerId;
              
           
        }
    }
}
