using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VotingModelLibrary.Models
{
    public class Election
    {
        [Key]
        [Display(Name = "ElectionId")]
        public int ElectionId { get; set; }

        [Display (Name = "StartDate")]
        public string DateStart { get; set; }

        [Display(Name = "EndDate")]
        public string DateEnd { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
