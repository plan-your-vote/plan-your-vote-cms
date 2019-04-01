using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<Candidate> Candidates { get; set; }
    }
}
