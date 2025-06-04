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
        public String TeamName { get; set; }
        public Team()
        {

        }
    }
}
