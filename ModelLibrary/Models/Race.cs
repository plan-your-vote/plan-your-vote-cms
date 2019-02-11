using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLibrary.Models
{
    public class Race
    {
        [Key]
        string positionName { get; set; }
        int numberNeeded { get; set; }

        public List<CandiateRace> CandidateRaces { get; } = new List<CandiateRace>();
    }
}
