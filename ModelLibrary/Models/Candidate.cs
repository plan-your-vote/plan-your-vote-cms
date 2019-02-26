using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLibrary.Models
{
    public class Candidate
    {
        [Key]
        int candidateId { get; set; }
        string firstName { get; set; }
        string lastName { get; set; }
        string picture { get; set; }
        string biography { get; set; }

        string organizationName { get; set; }
        Organization organization { get; set; }

        public List<CandiateRace> CandidateRaces { get; } = new List<CandiateRace>();
    }
}
