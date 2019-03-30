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

        [Display(Name = "Additional Info")]
        public string AdditionalInfo { get; set; }
        public string Address { get; set; }

        [Display(Name = "Wheelchair Info")]
        public string WheelchairInfo { get; set; }

        [Display(Name = "Parking Info")]
        public string ParkingInfo { get; set; }

        [Display(Name = "Parking Info")]
        public string WashroomInfo { get; set; }

        [Display(Name = "General Access Info")]
        public string GeneralAccessInfo { get; set; }
        public double Latitude { get; set; }
        public double Longitute { get; set; }
    }
}
