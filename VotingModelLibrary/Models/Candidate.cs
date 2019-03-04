using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLibrary.Models
{
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public string Biography { get; set; }
       
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public List<CandiateRace> CandidateRaces { get; } = new List<CandiateRace>();
    }
}
