using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tournament_tracker.Models
{
    public  class Prize
    {
        public int Id { get; set; }
        public int PlaceNumber { get; set; }
        public string PlaceName { get; set; }
        public decimal PrizeAmount { get; set; }
        public double PrizePercentage   { get; set; }

        public string PrizeString => PlaceName + " - " + PrizeAmount;

        public Prize()
        {
            
        }

        public Prize(int id,  int placeNumber, string placeName,decimal prizeAmount)
        {
            PlaceName = placeName;
            PlaceNumber = placeNumber;
            PrizeAmount = prizeAmount;
        }
    }
}
