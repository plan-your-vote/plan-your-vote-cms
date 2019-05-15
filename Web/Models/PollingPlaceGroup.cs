using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PollingPlaceGroup
    {
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

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Display(Name = "AdvanceOnly")]
        public bool AdvanceOnly { get; set; }

        [Display(Name = "LocalArea")]
        public string LocalArea { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "PollingPlaceDates")]
        public List<DateTime> PollingPlaceDates { get; set; }

        [Remote(action: "VerifyStartTime", controller: "PollingPlaces", AdditionalFields = nameof(PollingPlaceDates))]
        [Display(Name = "PollingStartTimes")]
        public List<DateTime> PollingStartTimes { get; set; }

        [Remote(action: "VerifyEndTime", controller: "PollingPlaces", AdditionalFields = nameof(PollingPlaceDates))]
        [Display(Name = "PollingEndTimes")]
        public List<DateTime> PollingEndTimes { get; set; }
    }
}
