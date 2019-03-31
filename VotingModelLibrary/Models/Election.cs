using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VotingModelLibrary.Models
{
    public class Election
    {
        [Key]
        public int ElectionId { get; set; }

        [Display (Name = "Start Date")]
        public string DateStart { get; set; }

        [Display(Name = "End Date")]
        public string DateEnd { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
