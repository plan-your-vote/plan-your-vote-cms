using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class PollingStationDate
    {
        [Key]
        public int PollingDateId { get; set; }

        [ForeignKey("PollingStationId")]
        [Display(Name = "PollingStationId")]
        public int PollingStationId { get; set; }
        public PollingStation PollingStation { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PollingDate { get; set; }

        [Display(Name = "StartTime")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "EndTime")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
    }
}
