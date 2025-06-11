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
     
        public int? TeamAId { get; set; }
        public int? TeamBId { get; set; }
        public int? TeamAScore { get; set; }
        public int? TeamBScore { get; set; }
        public int Id { get; set; }
        public int RoundNumber { get; set; }
        public int? WinnerId { get; set; }
        public int? TournamentId { get; set; }
        public bool AlreadyPlayed { get; set; }
        public Matchup( int id, int tournamenId,int? teamAId, int? teamBId, int? teamAScore, int? teamBScore, int roundNumber, int? winnerId)
        {
            
            TeamAId = teamAId;
            TeamBId = teamBId;            
            TeamAScore = teamAScore;
            TeamBScore = teamBScore;
            Id = id;
            RoundNumber=roundNumber;
            WinnerId=winnerId;
            TournamentId = tournamenId;
        }

        public Matchup()
        { }

        public void AddMatchupEntry( MatchupEntry entry )
        {
            Entries.Add( entry );   
        }

    }
}
