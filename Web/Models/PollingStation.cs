using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models
{
    public class PollingStation
    {
        [Key]
        [Display(Name = "PollingStationId")]
        public int PollingStationId { get; set; }

        [Display(Name = "ElectionId")]
        public int ElectionId { get; set; }

        [Display(Name = "Election")]
        public Election Election { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "AdditionalInfo")]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "WheelchairInfo")]
        public string WheelchairInfo { get; set; }

        [Display(Name = "ParkingInfo")]
        public string ParkingInfo { get; set; }

        [Display(Name = "WashroomInfo")]
        public string WashroomInfo { get; set; }

        [Display(Name = "GeneralAccessInfo")]
        public string GeneralAccessInfo { get; set; }

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Display(Name = "PollingStationDates")]
        public List<PollingStationDate> PollingStationDates { get; set; }
    }
}
