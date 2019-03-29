using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VotingModelLibrary.Models
{
    public class PollingStation
    {
        [Key]
        public int PollingStationId { get; set; }
        public int ElectionId { get; set; }
        public Election Election { get; set; }

        public string Name { get; set; }
        public string AdditionalInfo { get; set; }
        public string Address { get; set; }
        public string WheelchairInfo { get; set; }
        public string ParkingInfo { get; set; }
        public string WashroomInfo { get; set; }
        public string GeneralAccessInfo { get; set; }
        public double Latitude { get; set; }
        public double Longitute { get; set; }
    }
}
