using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tournament_tracker.Models
{
    public  class Prize
    {
        public int PlaceNumber { get; set; }
        public string PlaceName { get; set; }
        public double PrizeAmount { get; set; }
        public double PrizePercentage   { get; set; }
    }
}
