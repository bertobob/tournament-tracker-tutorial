using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tournament_tracker.Models
{
    public class Team
    {
        public List<Person> TeamMembers { get; set; } = new List<Person>();
        public int Id { get; set; }
        public string TeamName { get; set; }
        public Team()
        {

        }

        public Team(int id, string teamName)
        {
            Id = id;
            TeamName = teamName;
        }
    }
}
