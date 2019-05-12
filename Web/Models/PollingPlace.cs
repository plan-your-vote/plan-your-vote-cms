using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models
{
    public class PollingPlace
    {
        [Key]
        [Display(Name = "PollingPlaceId")]
        public int PollingPlaceId { get; set; }

        [Display(Name = "ElectionId")]
        public int ElectionId { get; set; }

        [Display(Name = "Election")]
        public Election Election { get; set; }

        [Display(Name = "Polling Place")]
        public string PollingPlaceName { get; set; }

        [Display(Name = "Polling Station")]
        public string PollingStationName { get; set; }

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

        [Display(Name = "PollingPlaceDates")]
        public List<PollingPlaceDate> PollingPlaceDates { get; set; }
    }
}
