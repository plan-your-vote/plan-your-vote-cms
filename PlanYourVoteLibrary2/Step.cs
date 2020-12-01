using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Step
    {
        public int ID { get; set; }

        [ForeignKey("ElectionId")]
        public int ElectionId { get; set; }

        [Display(Name = "StepNumber")]
        [Range(1,Int32.MaxValue)]
        public int StepNumber { get; set; }

        [Display(Name = "StepTitle")]
        public string StepTitle { get; set; }

        [Display(Name = "StepDescription")]
        public string StepDescription { get; set; }
    }
}
