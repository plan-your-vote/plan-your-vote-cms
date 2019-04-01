using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models
{
    public class Race
    {
        [Key]
        public int RaceId { get; set; }
        public int ElectionId { get; set; }
        public Election Election { get; set; }

        public string PositionName { get; set; }
        public int NumberNeeded { get; set; }

        public List<CandidateRace> CandidateRaces { get; set; }
    }
}
