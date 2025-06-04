using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tournament_tracker.Models
{
    public class MatchupEntry
    {
        /// <summary>
        /// Represents 1 team in the matchup
        /// </summary>
        public Team TeamCompeting {  get; set; }
        /// <summary>
        /// Represents the score for this particualar Team
        /// </summary>
        public double Score {  get; set; }
        /// <summary>
        /// Represents the matchup that this team cam from
        /// as winner
        /// </summary>
        public Matchup ParentMatchup { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialScore">
        /// </param>
        public MatchupEntry(double initialScore)
        {

        }
    }
}
