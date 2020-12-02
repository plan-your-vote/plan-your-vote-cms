using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace PlanYourVoteLibrary2

{
    public class PollingPlaceDate
    {
        [Key]
        public int PollingDateId { get; set; }

        [Display(Name = "PollingPlaceId")]
        public int PollingPlaceId { get; set; }

        public PollingPlace PollingPlace { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = false)]
        public DateTime PollingDate { get; set; }

        [Display(Name = "StartTime")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = false)]
        public DateTime StartTime { get; set; }

        [Display(Name = "EndTime")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = false)]
        public DateTime EndTime { get; set; }
    }
}
