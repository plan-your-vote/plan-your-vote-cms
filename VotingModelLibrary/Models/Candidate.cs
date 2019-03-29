using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models
{
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }
        public int ElectionId { get; set; }

        [Display (Name="FirstName")]
        public string FirstName { get; set; }
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
        [Display(Name = "Biography")]
        public string Biography { get; set; }
        [Display(Name = "OrganizationId")]
        public int OrganizationId { get; set; }
        [Display(Name = "Organization")]
        public Organization Organization { get; set; }

        public List<CandidateRace> CandidateRaces { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
