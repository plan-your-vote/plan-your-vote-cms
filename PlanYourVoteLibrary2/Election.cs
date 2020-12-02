using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlanYourVoteLibrary2

{
    public class Election
    {
        [Key]
        [Display(Name = "ElectionId")]
        public int ElectionId { get; set; }

        [Display(Name = "StartDate")]
        public DateTime StartDate { get; set; }

        [Display(Name = "EndDate")]
        public DateTime EndDate { get; set; }

        [Display(Name = "ElectionName")]
        public string ElectionName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
