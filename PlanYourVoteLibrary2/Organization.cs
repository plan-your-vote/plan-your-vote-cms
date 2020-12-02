using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PlanYourVoteLibrary2

{
    public class Organization
    {
        [Key]
        [Display(Name = "OrganizationId")]
        public int OrganizationId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Candidates")]
        public List<Candidate> Candidates { get; set; }
    }
}
