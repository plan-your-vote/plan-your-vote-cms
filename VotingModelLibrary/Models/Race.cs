using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models
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
        [Display(Name = "PositionName")]
        public string PositionName { get; set; }
        [Display(Name = "NumberNeeded")]
        public int NumberNeeded { get; set; }
        [Display(Name = "CandidateRaces")]
        public List<CandidateRace> CandidateRaces { get; set; }
    }
}
