﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Race
    {
        [Key]
        [Display(Name = "RaceId")]
        public int RaceId { get; set; }

        [Display(Name = "ElectionId")]
        public int ElectionId { get; set; }

        [Display(Name = "Election")]
        public Election Election { get; set; }

        [Display(Name = "BallotOrder")]
        public int BallotOrder { get; set; }

        [Display(Name = "PositionName")]
        public string PositionName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "NumberNeeded")]
        [Range(1, Int32.MaxValue)]
        public int NumberNeeded { get; set; }

        [Display(Name = "CandidateRaces")]
        public List<CandidateRace> CandidateRaces { get; set; }
    }
}
