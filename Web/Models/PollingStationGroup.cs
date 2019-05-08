using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PollingStationGroup
    {
        [Display(Name = "PollingStationName")]
        public string PollingStationName { get; set; }

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

        [Display(Name = "Longitute")]
        public double Longitute { get; set; }

        [Display(Name = "PollingStationDates")]
        public List<DateTime> PollingStationDates { get; set; }

        [Microsoft.AspNetCore.Mvc.Remote(action: "VerifyStartTime", controller: "PollingStations", AdditionalFields = nameof(PollingStationDates))]
        [Display(Name = "PollingStartTimes")]
        public List<DateTime> PollingStartTimes { get; set; }

        [Microsoft.AspNetCore.Mvc.Remote(action: "VerifyEndTime", controller: "PollingStations", AdditionalFields = nameof(PollingStationDates))]
        [Display(Name = "PollingEndTimes")]
        public List<DateTime> PollingEndTimes { get; set; }
    }
}
