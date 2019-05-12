using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class JSONPollingPlaces
    {
        public int VotingPlaceID { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool AdvanceOnly { get; set; }
        public string LocalArea { get; set; }
    }
}
