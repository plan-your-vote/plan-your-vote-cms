using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLibrary.Models
{
    public class Race
    {
        [Key]
        public string PositionName { get; set; }
        public int NumberNeeded { get; set; }

        public List<CandiateRace> CandidateRaces { get; } = new List<CandiateRace>();
    }
}
