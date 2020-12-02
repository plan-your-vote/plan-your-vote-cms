using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PlanYourVoteLibrary2

{
    public class JSONPollingPlace
    {
        public int VotingPlaceID { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool AdvanceOnly { get; set; }
        public string LocalArea { get; set; }
        public string WheelchairAccess { get; set; }
        public string Parking { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<JSONPollingPlaceDate> PollingPlaceDates { get; set; }
    }

    public class JSONPollingPlaceDate
    {
        public string PollingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
